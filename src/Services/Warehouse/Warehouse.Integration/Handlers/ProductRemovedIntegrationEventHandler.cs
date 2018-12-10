using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Integration.Events;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Integration.Handlers
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
