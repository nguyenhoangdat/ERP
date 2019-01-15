using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Integration.Events;
using System;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class ProductRenamedIntegrationEventHandler : IIntegrationEventHandler<ProductRenamedIntegrationEvent>
    {
        protected DatabaseContext DatabaseContext { get; }
        protected ILogger<ProductRenamedIntegrationEventHandler> Logger { get; }
        protected IMediator Mediator { get; }

        public ProductRenamedIntegrationEventHandler(
            DatabaseContext context,
            ILogger<ProductRenamedIntegrationEventHandler> logger,
            IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Logger = logger;
            this.Mediator = mediator;
        }

        public async Task Handle(ProductRenamedIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
