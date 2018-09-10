using Restmium.ERP.BuildingBlocks.EventBus.Events;

namespace Catalog.API.Integration.Events
{
    public class ProductAddedToBasketIntegrationEvent : IntegrationEvent
    {
        public int BasketId { get; }
        public int ProductId { get; }
        public int Units { get; }
    }
}
