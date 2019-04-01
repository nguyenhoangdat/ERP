using Restmium.Messaging;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Integration.Supply
{
    public class SuppliesOrderedIntegrationEvent : IntegrationEvent
    {
        public SuppliesOrderedIntegrationEvent() : base()
        {

        }
        public SuppliesOrderedIntegrationEvent(int warehouseId, string supplierName, DateTime utcExpected, IEnumerable<SupplyItem> items) : this()
        {
            this.WarehouseId = warehouseId;
            this.SupplierName = supplierName;
            this.UtcExpected = utcExpected;
            this.Items = items;
        }

        public int WarehouseId { get; }
        public string SupplierName { get; set; }
        public DateTime UtcExpected { get; set; }
        public IEnumerable<SupplyItem> Items { get; set; }

        public class SupplyItem
        {
            public int ProductId { get; set; }
            public int Count { get; set; }
        }
    }
}
