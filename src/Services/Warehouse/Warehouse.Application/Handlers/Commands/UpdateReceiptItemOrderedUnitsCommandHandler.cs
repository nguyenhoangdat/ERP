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
    public class UpdateReceiptItemOrderedUnitsCommandHandler : IRequestHandler<UpdateReceiptItemOrderedUnitsCommand, Receipt.Item>
    {
        public UpdateReceiptItemOrderedUnitsCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Receipt.Item> Handle(UpdateReceiptItemOrderedUnitsCommand request, CancellationToken cancellationToken)
        {
            Receipt.Item item = this.DatabaseContext.ReceiptItems.FirstOrDefault(x =>
                x.ReceiptId == request.ReceiptId &&
                x.PositionId == request.PositionId &&
                x.WareId == request.WareId);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["ReceiptItem_EntityNotFoundException"], request.ReceiptId, request.PositionId, request.WareId));
            }

            item.CountOrdered = request.OrderedUnits;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            // TODO: 2019.2 Publish Notification

            return item;
        }
    }
}
