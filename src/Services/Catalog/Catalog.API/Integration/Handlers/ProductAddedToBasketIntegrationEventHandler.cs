using Catalog.API.Integration.Events;
using Catalog.API.Models;
using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using System.Threading.Tasks;

namespace Catalog.API.Integration.Handlers
{
    public class ProductAddedToBasketIntegrationEventHandler : IIntegrationEventHandler<ProductAddedToBasketIntegrationEvent>
    {
        private DatabaseContext _databaseContext { get; set; }
        private ILogger<ProductAddedToBasketIntegrationEventHandler> _logger { get; }

        public ProductAddedToBasketIntegrationEventHandler(DatabaseContext context, ILogger<ProductAddedToBasketIntegrationEventHandler> logger)
        {
            _databaseContext = context;
            _logger = logger;
        }

        public async Task Handle(ProductAddedToBasketIntegrationEvent @event)
        {
            Product product = await _databaseContext.Products.FindAsync(@event.ProductId);

            product.AvailableUnits -= @event.Units;
            product.ReservedUnits += @event.Units;

            BasketReservation reservation = new BasketReservation()
            {
                BasketId = @event.BasketId,
                ProductId = @event.ProductId,
                Units = @event.Units
            };

            _databaseContext.BasketReservations.Add(reservation);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
