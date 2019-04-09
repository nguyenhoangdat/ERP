using Restmium.Messaging;

namespace Restmium.ERP.Integration.Warehouse
{
    public class WareRenamedIntegrationEvent : IntegrationEvent
    {
        public WareRenamedIntegrationEvent() : base()
        {

        }
        public WareRenamedIntegrationEvent(int productId) : this()
        {
            this.ProductId = productId;
        }

        public int ProductId { get; set; }
    }
}
