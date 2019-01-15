using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Integration.Events;
using System;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class ProductAddedIntegrationEventHandler : IIntegrationEventHandler<ProductAddedIntegrationEvent>
    {
        protected DatabaseContext DatabaseContext { get; }
        protected ILogger<ProductAddedIntegrationEventHandler> Logger { get; }
        protected IMediator Mediator { get; }

        public ProductAddedIntegrationEventHandler(
            DatabaseContext context,
            ILogger<ProductAddedIntegrationEventHandler> logger,
            IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Logger = logger;
            this.Mediator = mediator;
        }

        public async Task Handle(ProductAddedIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
