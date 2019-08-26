using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class StoreUnitsForReceiptItemAtPositionCommandHandler : IRequestHandler<StoreUnitsForReceiptItemAtPositionCommand, Receipt.Item>
    {
        public StoreUnitsForReceiptItemAtPositionCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Receipt.Item> Handle(StoreUnitsForReceiptItemAtPositionCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Receipt.Item> items = this.DatabaseContext.ReceiptItems.Where(x =>
                x.ReceiptId == request.ReceiptId &&
                x.WareId == request.WareId);

            if (items.Count() == 0)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.ReceiptItem_EntitiesNotFoundException, request.ReceiptId, request.WareId));
            }

            Receipt.Item item = items.FirstOrDefault(x => x.PositionId == request.PositionId);
            Receipt.Item unassignedItem = items.FirstOrDefault(x => x.PositionId == null);

            if (item == null)
            {
                if (request.Count <= unassignedItem.CountOrdered - unassignedItem.CountReceived)
                {
                    item = await this.Mediator.Send(new AssignReceiptItemToPositionCommand(unassignedItem.ReceiptId, request.PositionId, unassignedItem.WareId), cancellationToken);
                }
                else
                {
                    throw new EntityNotFoundException(string.Format(Properties.Resources.ReceiptItem_EntityNotFoundException, request.ReceiptId, request.PositionId, request.WareId));
                }
            }

            int unitsRemainingToStore = item.CountOrdered - item.CountReceived;
            if (item.CountOrdered < request.Count) // Trying to store more units than ordered
            {
                int extraUnits = request.Count - (item.CountOrdered - item.CountReceived);
                unassignedItem = items.FirstOrDefault(x => x.PositionId == null);

                if (unassignedItem != null && extraUnits <= (unassignedItem.CountOrdered - unassignedItem.CountReceived))
                {
                    // Transfer units
                    await this.Mediator.Send(new UpdateReceiptItemOrderedUnitsCommand(unassignedItem.ReceiptId, unassignedItem.PositionId, unassignedItem.WareId, unassignedItem.CountOrdered - extraUnits), cancellationToken);
                    await this.Mediator.Send(new UpdateReceiptItemOrderedUnitsCommand(item.ReceiptId, item.PositionId, item.WareId, item.CountOrdered + extraUnits), cancellationToken);

                    item.CountReceived += request.Count;
                    await this.DatabaseContext.SaveChangesAsync(cancellationToken);

                    await this.Mediator.Send(new CreateMovementCommand(item.WareId, request.PositionId, Movement.Direction.In, request.Count), cancellationToken);

                }
                else
                {
                    throw new UnitsExceededException(string.Format(Properties.Resources.ReceiptItem_OrderedUnitsExceededException, request.ReceiptId, request.PositionId, request.WareId));
                }
            }
            else
            {
                item.CountReceived += request.Count;
                await this.DatabaseContext.SaveChangesAsync(cancellationToken);

                if (item.CountReceived < item.CountOrdered)
                {
                    unitsRemainingToStore = item.CountOrdered - item.CountReceived;

                    unassignedItem = items.FirstOrDefault(x => x.PositionId == null);
                    if (unassignedItem == null)
                    {
                        await this.Mediator.Send(new CreateReceiptItemCommand(item.WareId, null, item.ReceiptId, unitsRemainingToStore, 0), cancellationToken);
                    }
                    else
                    {
                        await this.Mediator.Send(new UpdateReceiptItemOrderedUnitsCommand(unassignedItem.ReceiptId, unassignedItem.PositionId, unassignedItem.WareId, unassignedItem.CountOrdered + unitsRemainingToStore), cancellationToken);
                    }

                    await this.Mediator.Send(new UpdateReceiptItemOrderedUnitsCommand(item.ReceiptId, item.PositionId, item.WareId, item.CountOrdered - unitsRemainingToStore), cancellationToken);
                }

                await this.Mediator.Send(new CreateMovementCommand(item.WareId, request.PositionId, Movement.Direction.In, request.Count), cancellationToken);
            }

            return item;
        }
    }
}
