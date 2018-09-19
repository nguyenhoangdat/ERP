using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Integration.Events
{
    /// <summary>
    /// Event produced by Warehouse.API
    /// </summary>
    public class OrderStockRejectedIntegrationEvent
    {
        public long OrderId { get; set; }
    }
}
