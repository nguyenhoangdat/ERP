using Restmium.ERP.BuildingBlocks.EventBus.Events;

namespace Restmium.ERP.Integration.Catalog
{
    /// <summary>
    /// Integration event produced by Catalog.API when Product is removed.
    /// </summary>
    public class ProductRemovedIntegrationEvent : IntegrationEvent
    {
        public ProductRemovedIntegrationEvent()
        {

        }
        public ProductRemovedIntegrationEvent(int productId) : base()
        {
            this.ProductId = productId;
        }

        public int ProductId { get; set; }
    }
}
