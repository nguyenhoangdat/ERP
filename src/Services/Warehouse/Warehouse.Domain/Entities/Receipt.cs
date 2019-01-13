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
        public Receipt(long id, string name, DateTime utcExpected) : this()
        {
            this.Id = id;
            this.Name = name;
            this.UtcExpected = utcExpected;
        }
        public Receipt(long id, string name, DateTime utcExpected, ICollection<Item> items) : this(id, name, utcExpected)
        {
            this.Items = items;
        }
        public Receipt(long id, string name, DateTime utcExpected, DateTime? utcReceived) : this(id, name, utcExpected)
        {
            this.UtcReceived = utcReceived;
        }
        public Receipt(long id, string name, DateTime utcExpected, DateTime? utcReceived, ICollection<Item> items) : this(id, name, utcExpected, utcReceived)
        {
            this.Items = items;
        }

        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime UtcExpected { get; set; }
        public DateTime? UtcReceived { get; set; }

        public virtual ICollection<Item> Items { get; protected set; }
    }
}
