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
        public long OrderId { get; set; }
        public DateTime UtcDispatchDate { get; set; } //Odeslat dne
        public DateTime UtcDeliveryDate { get; set; } //Dodat dne

        public List<IssueSlipItem> Items { get; set; }

        public class IssueSlipItem
        {
            public int ProductId { get; set; }
            public int Units { get; set; }

            public double Width { get; set; }
            public double Height { get; set; }
            public double Depth { get; set; }

            public double Weight { get; set; }
        }
    }
}
