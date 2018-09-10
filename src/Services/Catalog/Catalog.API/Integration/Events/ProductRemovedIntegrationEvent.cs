using Restmium.ERP.BuildingBlocks.EventBus.Events;

namespace Catalog.API.Integration.Events
{
    public class ProductRemovedIntegrationEvent : IntegrationEvent
    {
        public ProductRemovedIntegrationEvent(int productId) : base()
        {
            this.ProductId = productId;
        }

        public int ProductId { get; }
    }
}
