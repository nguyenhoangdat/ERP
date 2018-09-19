using Restmium.ERP.BuildingBlocks.EventBus.Events;

namespace Ordering.API.Integration.Events
{
    public class OrderStockConfirmedIntegrationEvent : IntegrationEvent
    {
        public OrderStockConfirmedIntegrationEvent(long orderId) : base()
        {
            OrderId = orderId;
        }

        public long OrderId { get; set; }
    }
}
