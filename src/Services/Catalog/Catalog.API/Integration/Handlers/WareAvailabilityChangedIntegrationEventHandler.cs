using Catalog.API.Integration.Events;
using Catalog.API.Models;
using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using System.Threading.Tasks;

namespace Catalog.API.Integration.Handlers
{
    public class WareAvailabilityChangedIntegrationEventHandler : IIntegrationEventHandler<WareAvailabilityChangedIntegrationEvent>
    {
        private DatabaseContext _databaseContext { get; }
        private ILogger<WareAvailabilityChangedIntegrationEventHandler> _logger { get; }

        public WareAvailabilityChangedIntegrationEventHandler(DatabaseContext context, ILogger<WareAvailabilityChangedIntegrationEventHandler> logger)
        {
            _databaseContext = context;
            _logger = logger;
        }

        public async Task Handle(WareAvailabilityChangedIntegrationEvent @event)
        {
            Product product = await _databaseContext.Products.FindAsync(@event.ProductId);

            if (product != null)
            {
                product.AvailableUnits = @event.Units;
                await _databaseContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogCritical("Product with Id {0} not found!", @event.ProductId);
            }
        }
    }
}
