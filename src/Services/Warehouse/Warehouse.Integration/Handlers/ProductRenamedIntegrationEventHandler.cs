using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Integration.Events;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Integration.Handlers
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
            Ware ware = this._databaseContext.Wares.Find(@event.ProductId);

            if (ware != null)
            {
                ware.ProductName = @event.ProductName;
                await this._databaseContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogCritical("Ware with ProductId {0} not found!", @event.ProductId);
            }
        }
    }
}
