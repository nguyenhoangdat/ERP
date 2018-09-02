using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.API.Integration.Events;
using Warehouse.API.Models;

namespace Warehouse.API.Integration.Handlers
{
    public class ProductAddedIntegrationEventHandler : IIntegrationEventHandler<ProductAddedIntegrationEvent>
    {
        private DatabaseContext _databaseContext { get; }

        public ProductAddedIntegrationEventHandler(DatabaseContext context)
        {
            this._databaseContext = context;
        }

        public async Task Handle(ProductAddedIntegrationEvent @event)
        {
            if (!this._databaseContext.Wares.Any(w => w.ProductId == @event.ProductId))
            {
                Ware ware = new Ware()
                {
                    ProductId = @event.ProductId,
                    ProductName = @event.ProductName
                };

                this._databaseContext.Wares.Add(ware);
                await this._databaseContext.SaveChangesAsync();
            }
        }
    }
}
