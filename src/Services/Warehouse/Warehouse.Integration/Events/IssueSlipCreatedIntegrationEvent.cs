using Restmium.ERP.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Integration.Events
{
    /// <summary>
    /// Event produced by Warehouse.API
    /// </summary>
    public class IssueSlipCreatedIntegrationEvent : IntegrationEvent
    {
        public IssueSlipCreatedIntegrationEvent(long orderId, DateTime utcDispatchDate, DateTime utcDeliveryDate)
        {
            this.OrderId = orderId;
            this.UtcDispatchDate = utcDispatchDate;
            this.UtcDeliveryDate = utcDeliveryDate;
        }

        public long OrderId { get; }
        public DateTime UtcDispatchDate { get; } //Odeslat dne
        public DateTime UtcDeliveryDate { get; } //Dodat dne

        public List<IssueSlipItem> Items { get; }

        public class IssueSlipItem
        {
            public IssueSlipItem(int productId, int units, double width, double height, double depth, double weight)
            {
                this.ProductId = productId;
                this.Units = units;
                this.Width = width;
                this.Height = height;
                this.Depth = depth;
                this.Weight = weight;
            }

            public int ProductId { get; }
            public int Units { get; }

            public double Width { get; }
            public double Height { get; }
            public double Depth { get; }

            public double Weight { get; }
        }
    }
}
