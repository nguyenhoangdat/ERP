using Restmium.ERP.BuildingBlocks.EventBus.Events;
using System;
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

        //TODO: Check
        public DateTime UtcDispatchDate { get; set; } //Odeslat dne
        public DateTime UtcDeliveryDate { get; set; } //Dodat dne

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
