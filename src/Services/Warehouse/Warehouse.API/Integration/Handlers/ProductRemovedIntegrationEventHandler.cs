using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using System.Threading.Tasks;
using Warehouse.API.Integration.Events;
using Warehouse.API.Models;

namespace Warehouse.API.Integration.Handlers
{
    public class ProductRemovedIntegrationEventHandler : IIntegrationEventHandler<ProductRemovedIntegrationEvent>
    {
        private DatabaseContext _databaseContext { get; }

        public ProductRemovedIntegrationEventHandler(DatabaseContext context)
        {
            this._databaseContext = context;
        }

        public async Task Handle(ProductRemovedIntegrationEvent @event)
        {
            Ware ware = await _databaseContext.Wares.FindAsync(@event.ProductId);
            _databaseContext.Remove(ware);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
