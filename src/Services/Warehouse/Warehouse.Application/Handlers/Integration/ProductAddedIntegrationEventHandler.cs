using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Integration.Events;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class ProductAddedIntegrationEventHandler : IIntegrationEventHandler<ProductAddedIntegrationEvent>
    {
        protected DatabaseContext DatabaseContext { get; }
        protected ILogger<ProductAddedIntegrationEventHandler> Logger { get; }
        protected IMediator Mediator { get; }

        public ProductAddedIntegrationEventHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        public async Task Handle(ProductAddedIntegrationEvent @event)
        {
            // Create Ware
            Ware ware = await this.Mediator.Send(new CreateWareCommand(@event.ProductId, @event.ProductName));
        }
    }
}
