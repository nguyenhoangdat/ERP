using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using System.Threading.Tasks;
using Warehouse.API.Integration.Events;
using Warehouse.API.Models;

namespace Warehouse.API.Integration.Handlers
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
