namespace Restmium.ERP.Integration.Catalog
{
    /// <summary>
    /// Integration event produced by Catalog.API when Product is renamed.
    /// </summary>
    public class ProductRenamedIntegrationEvent : ProductCreatedIntegrationEvent
    {
        public ProductRenamedIntegrationEvent() : base()
        {
        }

        public ProductRenamedIntegrationEvent(int productId, string name) : base(productId, name)
        {
        }
    }
}
