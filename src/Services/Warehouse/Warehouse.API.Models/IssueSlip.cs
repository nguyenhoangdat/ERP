using Restmium.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.API.Models
{
    public partial class IssueSlip : DatabaseEntity
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public DateTime UtcDispatchDate { get; set; }

        [Required]
        public DateTime UtcDeliveryDate { get;set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
