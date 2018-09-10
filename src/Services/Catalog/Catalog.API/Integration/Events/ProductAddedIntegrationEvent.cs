using Restmium.ERP.BuildingBlocks.EventBus.Events;

namespace Catalog.API.Integration.Events
{
    public class ProductAddedIntegrationEvent : IntegrationEvent
    {
        public ProductAddedIntegrationEvent(int productId, string productName) : base()
        {
            this.ProductId = productId;
            this.ProductName = productName;
        }

        public int ProductId { get; }
        public string ProductName { get; }
    }
}
