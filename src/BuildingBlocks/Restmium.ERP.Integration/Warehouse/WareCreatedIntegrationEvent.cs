using Restmium.Messaging;

namespace Restmium.ERP.Integration.Warehouse
{
    public class WareCreatedIntegrationEvent : IntegrationEvent
    {
        public WareCreatedIntegrationEvent() : base()
        {

        }
        public WareCreatedIntegrationEvent(int productId) : this()
        {
            this.ProductId = productId;
        }

        public int ProductId { get; set; }
    }
}
