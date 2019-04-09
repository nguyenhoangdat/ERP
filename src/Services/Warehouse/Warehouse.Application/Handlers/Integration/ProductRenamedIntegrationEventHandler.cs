using MediatR;
using Restmium.ERP.Integration.Catalog;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.Messaging;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class ProductRenamedIntegrationEventHandler : IIntegrationEventHandler<ProductRenamedIntegrationEvent>
    {
        protected IMediator Mediator { get; }

        public ProductRenamedIntegrationEventHandler(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        public async Task Handle(ProductRenamedIntegrationEvent @event)
        {
            Ware ware = await this.Mediator.Send(new RenameWareCommand(@event.ProductId, @event.ProductName));

            await this.Mediator.Publish(new WareRenamedDomainEvent(ware));
        }
    }
}
