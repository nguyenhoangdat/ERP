using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using System.Threading.Tasks;
using Warehouse.API.Integration.Events;
using Warehouse.API.Models;

namespace Warehouse.API.Integration.Handlers
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
            Ware ware = await _databaseContext.Wares.FindAsync(@event.ProductId);

            if (ware != null)
            {
                _databaseContext.Remove(ware);
                await _databaseContext.SaveChangesAsync();
            }
            else
            {
                this._logger.LogError("Ware with ProductId {0} not found!", @event.ProductId);
            }
        }
    }
}
