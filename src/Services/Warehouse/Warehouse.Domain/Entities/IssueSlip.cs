using Restmium.ERP.BuildingBlocks.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public partial class IssueSlip : DatabaseEntity
    {
        public IssueSlip()
        {
            this.Items = new HashSet<Item>();
        }
        public IssueSlip(long orderId, DateTime utcDispatchDate, DateTime utcDeliveryDate) : this()
        {
            this.OrderId = orderId;
            this.UtcDispatchDate = utcDispatchDate;
            this.UtcDeliveryDate = utcDeliveryDate;
        }
        public IssueSlip(long orderId, DateTime utcDispatchDate, DateTime utcDeliveryDate, ICollection<Item> items) : this(orderId, utcDispatchDate, utcDeliveryDate)
        {
            this.Items = items;
        }

        [Required]
        public long Id { get; set; }

        [Required]
        public long OrderId { get; set; }

        [Required]
        public DateTime UtcDispatchDate { get; set; } // Naplánované datum odeslání

        [Required]
        public DateTime UtcDeliveryDate { get; set; } // Naplánované datum doručení (dodat dne)

        public DateTime? UtcProcessed { get; set; } // Datum a čas zprocesování objednávky
        public DateTime? UtcDispatched { get; set; } // Datum a čas odeslání objednávky

        public virtual ICollection<Item> Items { get; protected set; }
    }
}
