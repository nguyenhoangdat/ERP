using Restmium.Messaging;

namespace Restmium.ERP.Integration.Warehouse
{
    public class WareDeletedIntegrationEvent : IntegrationEvent
    {
        public WareDeletedIntegrationEvent() : base()
        {

        }
        public WareDeletedIntegrationEvent(int productId) : this()
        {
            this.ProductId = productId;
        }

        public int ProductId { get; set; }
    }
}
