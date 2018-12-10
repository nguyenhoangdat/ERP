using Restmium.ERP.BuildingBlocks.EventBus.Events;

namespace Restmium.ERP.Services.Warehouse.Integration.Events
{
    /// <summary>
    /// Event produced by Warehouse.API
    /// </summary>
    public class OrderStockConfirmedIntegrationEvent : IntegrationEvent
    {
        public OrderStockConfirmedIntegrationEvent(long orderId) : base()
        {
            this.OrderId = orderId;
        }

        public long OrderId { get; set; }
    }
}
