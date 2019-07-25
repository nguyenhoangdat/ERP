using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateReceiptCommandHandler : IRequestHandler<CreateReceiptCommand, Receipt>
    {
        public CreateReceiptCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Receipt> Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
        {
            List<Receipt.Item> items = new List<Receipt.Item>();
            foreach (CreateReceiptCommand.Item item in request.Items)
            {
                items.Add(new Receipt.Item(0, null, item.WareId, item.CountOrdered, 0));
            }

            Receipt receipt = this.DatabaseContext.Receipts.Add(new Receipt(0, request.UtcExpected, items)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new ReceiptCreatedDomainEvent(receipt), cancellationToken);

            return receipt;
        }
    }
}
