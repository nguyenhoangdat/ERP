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
        private DatabaseContext _databaseContext { get; }
        private ILogger<ProductRemovedIntegrationEventHandler> _logger { get; }

        public ProductRemovedIntegrationEventHandler(DatabaseContext context, ILogger<ProductRemovedIntegrationEventHandler> logger)
        {
            this._databaseContext = context;
            this._logger = logger;
        }

        public async Task Handle(ProductRemovedIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
