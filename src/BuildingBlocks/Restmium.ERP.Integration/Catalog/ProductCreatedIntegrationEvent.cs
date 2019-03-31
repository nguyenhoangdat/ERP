using Restmium.Messaging;

namespace Restmium.ERP.Integration.Catalog
{
    /// <summary>
    /// Integration event produced by Catalog.API when Product is created.
    /// </summary>
    public class ProductCreatedIntegrationEvent : IntegrationEvent
    {
        public ProductCreatedIntegrationEvent()
        {

        }
        public ProductCreatedIntegrationEvent(int productId, string name) : base()
        {
            this.ProductId = productId;
            this.ProductName = name;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
