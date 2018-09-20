using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.API.Integration.Events;
using Warehouse.API.Models;

namespace Warehouse.API.Integration.Handlers
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
            else
            {
                this._logger.LogWarning("Ware with ProductId {0} already exists!", @event.ProductId);
            }
        }
    }
}
