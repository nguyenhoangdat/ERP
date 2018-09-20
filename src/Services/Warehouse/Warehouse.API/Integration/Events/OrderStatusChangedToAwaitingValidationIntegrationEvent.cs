using Restmium.ERP.BuildingBlocks.EventBus.Events;
using System.Collections.Generic;

namespace Warehouse.API.Integration.Events
{
    /// <summary>
    /// Event produced by Ordering.API
    /// </summary>
    public class OrderStatusChangedToAwaitingValidationIntegrationEvent : IntegrationEvent
    {
        public long OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public class OrderItem
        {
            public OrderItem(int productId, int units)
            {
                this.ProductId = productId;
                this.Units = units;
            }

            public int ProductId { get; set; }
            public int Units { get; set; }
        }
    }
}
