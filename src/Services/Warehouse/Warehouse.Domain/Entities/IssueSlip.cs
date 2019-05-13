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

        public DateTime UtcDispatchDate { get; set; }

        [Required]
        public DateTime UtcDeliveryDate { get; set; }

        public virtual ICollection<Item> Items { get; protected set; }
    }
}
