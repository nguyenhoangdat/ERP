using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Integration.Events;
using System;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class ProductRemovedIntegrationEventHandler : IIntegrationEventHandler<ProductRemovedIntegrationEvent>
    {
        protected DatabaseContext DatabaseContext { get; }
        protected ILogger<ProductRemovedIntegrationEventHandler> Logger { get; }
        protected IMediator Mediator { get; }

        public ProductRemovedIntegrationEventHandler(
            DatabaseContext context,
            ILogger<ProductRemovedIntegrationEventHandler> logger,
            IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Logger = logger;
            this.Mediator = mediator;
        }

        public async Task Handle(ProductRemovedIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
