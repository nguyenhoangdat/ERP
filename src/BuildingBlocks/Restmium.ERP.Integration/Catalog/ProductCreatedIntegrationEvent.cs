using Restmium.Messaging;

namespace Restmium.ERP.Integration.Catalog
{
    /// <summary>
    /// Integration event produced by Catalog.API when Product is created.
    /// </summary>
    public class ProductCreatedIntegrationEvent : IntegrationEvent
    {
        public ProductCreatedIntegrationEvent() : base()
        {

        }
        public ProductCreatedIntegrationEvent(int productId, string name) : this()
        {
            this.ProductId = productId;
            this.ProductName = name;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
