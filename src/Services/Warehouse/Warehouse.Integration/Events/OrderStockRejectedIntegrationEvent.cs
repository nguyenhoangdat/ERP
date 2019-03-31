using Restmium.Messaging;

namespace Restmium.ERP.Services.Warehouse.Integration.Events
{
    /// <summary>
    /// Event produced by Warehouse.API
    /// </summary>
    public class OrderStockRejectedIntegrationEvent : IntegrationEvent
    {
        public OrderStockRejectedIntegrationEvent(long orderId) : base()
        {
            this.OrderId = orderId;
        }

        public long OrderId { get; set; }
    }
}
