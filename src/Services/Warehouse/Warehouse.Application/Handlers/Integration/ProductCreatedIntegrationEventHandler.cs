using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.Integration.Catalog;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.Messaging;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class ProductCreatedIntegrationEventHandler : IIntegrationEventHandler<ProductCreatedIntegrationEvent>
    {
        protected DatabaseContext DatabaseContext { get; }
        protected ILogger<ProductCreatedIntegrationEventHandler> Logger { get; }
        protected IMediator Mediator { get; }

        public ProductCreatedIntegrationEventHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        public async Task Handle(ProductCreatedIntegrationEvent @event)
        {
            Ware ware = await this.Mediator.Send(new CreateWareCommand(@event.ProductId, @event.ProductName));

            await this.Mediator.Publish(new WareCreatedDomainEvent(ware));
        }
    }
}
