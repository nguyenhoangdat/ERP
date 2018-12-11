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
        protected DatabaseContext _databaseContext { get; }
        protected ILogger<ProductAddedIntegrationEventHandler> _logger { get; }

        public ProductAddedIntegrationEventHandler(DatabaseContext context, ILogger<ProductAddedIntegrationEventHandler> logger)
        {
            this._databaseContext = context;
            this._logger = logger;
        }

        public async Task Handle(ProductAddedIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
