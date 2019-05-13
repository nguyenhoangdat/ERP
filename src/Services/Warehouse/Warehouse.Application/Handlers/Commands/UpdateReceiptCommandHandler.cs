using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateReceiptCommandHandler : IRequestHandler<UpdateReceiptCommand, Receipt>
    {
        public UpdateReceiptCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Receipt> Handle(UpdateReceiptCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Receipts.Any(x => x.Id == request.Id))
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Receipt_Update_EntityNotFoundException"], request.Id));
            }

            List<Receipt.Item> items = new List<Receipt.Item>();
            foreach (UpdateReceiptCommand.Item item in request.Items)
            {
                items.Add(new Receipt.Item(item.ReceiptId, item.PositionId, item.WareId, item.CountOrdered, item.CountReceived, item.UtcProcessed));
            }

            Receipt receipt = this.DatabaseContext.Receipts.Update(new Receipt(request.Id, request.UtcExpected, request.UtcReceived, items)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new ReceiptUpdatedDomainEvent(receipt), cancellationToken);

            return receipt;
        }
    }
}