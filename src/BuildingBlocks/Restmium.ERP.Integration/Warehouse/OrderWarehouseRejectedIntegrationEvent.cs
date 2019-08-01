using Restmium.Messaging;

namespace Restmium.ERP.Integration.Warehouse
{
    public class OrderWarehouseRejectedIntegrationEvent : IntegrationEvent
    {
        public long OrderId { get; }

        protected OrderWarehouseRejectedIntegrationEvent() : base()
        {
        }

        public OrderWarehouseRejectedIntegrationEvent(long orderId) : this()
        {
            this.OrderId = orderId;
        }
    }
}
