using Restmium.ERP.BuildingBlocks.EventBus.Events;

namespace Warehouse.API.Integration.Events
{
    /// <summary>
    /// Event produced by Warehouse.API
    /// </summary>
    public class OrderStockPickingStartedIntegrationEvent : IntegrationEvent
    {
        public OrderStockPickingStartedIntegrationEvent(long orderId) : base()
        {
            this.OrderId = orderId;
        }

        public long OrderId { get; set; }
    }
}
