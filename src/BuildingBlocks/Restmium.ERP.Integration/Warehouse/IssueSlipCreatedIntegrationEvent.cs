using Restmium.Messaging;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Integration.Warehouse
{
    /// <summary>
    /// Integration event produced by Warehouse.API when Issue Slip is created
    /// </summary>
    public class IssueSlipCreatedIntegrationEvent : IntegrationEvent
    {
        public IssueSlipCreatedIntegrationEvent(long orderId, DateTime utcDispatchDate, DateTime utcDeliveryDate, IEnumerable<IssueSlipItem> items)
        {
            this.OrderId = orderId;
            this.UtcDispatchDate = utcDispatchDate;
            this.UtcDeliveryDate = utcDeliveryDate;
            this.Items = items;
        }

        public long OrderId { get; }
        public DateTime UtcDispatchDate { get; } //Odeslat dne
        public DateTime UtcDeliveryDate { get; } //Dodat dne

        public IEnumerable<IssueSlipItem> Items { get; }

        public class IssueSlipItem
        {
            public IssueSlipItem(int productId, int requestedUnits, double width, double height, double depth, double weight)
            {
                this.ProductId = productId;
                this.RequestedUnits = requestedUnits;
                this.Width = width;
                this.Height = height;
                this.Depth = depth;
                this.Weight = weight;
            }

            public int ProductId { get; }
            public int RequestedUnits { get; }

            public double Width { get; }
            public double Height { get; }
            public double Depth { get; }

            public double Weight { get; }
        }
    }
}
