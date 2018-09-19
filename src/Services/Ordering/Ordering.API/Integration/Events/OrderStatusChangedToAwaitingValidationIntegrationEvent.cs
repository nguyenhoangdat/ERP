using Restmium.ERP.BuildingBlocks.EventBus.Events;
using System.Collections.Generic;

namespace Ordering.API.Integration.Events
{
    public class OrderStatusChangedToAwaitingValidationIntegrationEvent : IntegrationEvent
    {
        public OrderStatusChangedToAwaitingValidationIntegrationEvent(bool partialSend, List<OrderItem> items) : base()
        {
            this.PartialSend = partialSend;
            this.OrderItems = items;
        }

        public bool PartialSend { get; set; }
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
