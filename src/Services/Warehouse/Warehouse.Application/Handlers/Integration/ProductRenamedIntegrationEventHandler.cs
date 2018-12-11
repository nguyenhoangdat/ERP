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
        private DatabaseContext _databaseContext { get; }
        private ILogger<ProductRenamedIntegrationEventHandler> _logger { get; }

        public ProductRenamedIntegrationEventHandler(DatabaseContext context, ILogger<ProductRenamedIntegrationEventHandler> logger)
        {
            _databaseContext = context;
            _logger = logger;
        }

        public async Task Handle(ProductRenamedIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
