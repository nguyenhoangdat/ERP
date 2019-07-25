using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateReceiptItemCommandHandler : IRequestHandler<CreateReceiptItemCommand, Receipt.Item>
    {
        public CreateReceiptItemCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Receipt.Item> Handle(CreateReceiptItemCommand request, CancellationToken cancellationToken)
        {
            Receipt.Item item = (await this.DatabaseContext.AddAsync(new Receipt.Item(request.ReceiptId, request.PositionId, request.WareId, request.CountOrdered, request.CountReceived), cancellationToken)).Entity;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new ReceiptItemCreatedDomainEvent(item), cancellationToken);

            return item;
        }
    }
}
