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

            Receipt.Item item = items.FirstOrDefault(x => x.PositionId == request.PositionId);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["ReceiptItem_EntityNotFoundException"], request.ReceiptId, request.PositionId, request.WareId));
            }

            // TODO: Assign position if there is some Receipt.Item with (x.PositionId=null && request.Count <= x.CountOrdered)

            int unitsRemainingToStore = item.CountOrdered - item.CountReceived;
            if (item.CountOrdered < request.Count)
            {
                // Trying to store more units than ordered
            }
            else
            {
                item.CountReceived += request.Count;
                await this.DatabaseContext.SaveChangesAsync(cancellationToken);

                if (item.CountReceived < item.CountOrdered)
                {
                    unitsRemainingToStore = item.CountOrdered - item.CountReceived;

                    Receipt.Item unassignedItem = items.FirstOrDefault(x => x.PositionId == null);
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
