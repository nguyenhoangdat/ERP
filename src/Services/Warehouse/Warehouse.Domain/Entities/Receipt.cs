using Restmium.ERP.BuildingBlocks.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public partial class Receipt : DatabaseEntity
    {
        public Receipt()
        {
            this.Items = new HashSet<Item>();
        }

        protected Receipt(long id, DateTime utcExpected) : this()
        {
            this.Id = id;
            this.UtcExpected = utcExpected;
        }
        protected Receipt(DateTime utcExpected) : this(0, utcExpected)
        {
        }

        public Receipt(long id, DateTime utcExpected, ICollection<Item> items) : this(id, utcExpected)
        {
            this.Items = items;
        }
        public Receipt(DateTime utcExpected, ICollection<Item> items) : this(0, utcExpected, items)
        {
        }

        public Receipt(long id, DateTime utcExpected, DateTime? utcReceived, ICollection<Item> items) : this(id, utcExpected, items)
        {
            this.UtcReceived = utcReceived;
        }
        public Receipt(DateTime utcExpected, DateTime? utcReceived, ICollection<Item> items) : this(0, utcExpected, utcReceived, items)
        {
        }

        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime UtcExpected { get; set; }
        public DateTime? UtcReceived { get; set; }

        public virtual ICollection<Item> Items { get; protected set; }
    }
}
