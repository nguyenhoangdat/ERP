using Restmium.Messaging;

namespace Restmium.ERP.Integration.Catalog
{
    /// <summary>
    /// Integration event produced by Catalog.API when Product is removed.
    /// </summary>
    public class ProductRemovedIntegrationEvent : IntegrationEvent
    {
        public ProductRemovedIntegrationEvent() : base()
        {

        }
        public ProductRemovedIntegrationEvent(int productId) : this()
        {
            this.ProductId = productId;
        }

        public int ProductId { get; set; }
    }
}
