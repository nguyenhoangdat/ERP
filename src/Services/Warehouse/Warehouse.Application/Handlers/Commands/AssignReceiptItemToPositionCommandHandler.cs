using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class AssignReceiptItemToPositionCommandHandler : IRequestHandler<AssignReceiptItemToPositionCommand, Receipt.Item>
    {
        public AssignReceiptItemToPositionCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Receipt.Item> Handle(AssignReceiptItemToPositionCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Receipt.Item> items = this.DatabaseContext.ReceiptItems.Where(x =>
                x.ReceiptId == request.ReceiptId &&
                x.PositionId == null &&
                x.WareId == request.WareId);

            if (items.Count() == 0)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["ReceiptItem_EntitiesNotFoundException"], request.ReceiptId, request.WareId));
            }

            if (items.Any(x => x.PositionId == request.PositionId))
            {
                throw new PositionAlreadyAssignedException(string.Format(Resources.Exceptions.Values["ReceiptItem_PositionAlreadyAssignedException"], request.ReceiptId, request.PositionId, request.WareId));
            }

            Receipt.Item item = items.FirstOrDefault(x => x.PositionId == null);

            if (item == null)
            {
                throw new FullyAssignedException(string.Format(Resources.Exceptions.Values["ReceiptItem_FullyAssignedException"], request.ReceiptId, request.WareId));
            }

            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == request.PositionId);

            if (position == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Position_EntityNotFoundException"], request.PositionId));
            }

            if (item.WareId != position.GetWare().Id && position.GetWare() != null)
            {
                throw new PositionWareConflictException(string.Format(Resources.Exceptions.Values["ReceiptItem_PositionWareConflictException"], request.ReceiptId, request.PositionId, request.WareId));
            }

            item.PositionId = request.PositionId;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new ReceiptItemAssignedToPositionDomainEvent(item), cancellationToken);

            return item;
        }
    }
}
