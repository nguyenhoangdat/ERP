using Restmium.ERP.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Integration.Events
{
    /// <summary>
    /// Integration Event created by Suppliers.API.
    /// Used for creating a Receipt for specified Warehouse
    /// </summary>
    public class SuppliesOrderedIntegrationEvent : IntegrationEvent
    {
        public int WarehouseId { get; }
        public string SupplierName { get; set; }
        public DateTime UtcExpected { get; set; }
        public List<SupplyItem> Items { get; set; }

        public class SupplyItem
        {
            public int ProductId { get; set; }
            public int Count { get; set; }
        }
    }
}
