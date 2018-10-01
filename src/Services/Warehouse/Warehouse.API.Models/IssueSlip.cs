using Restmium.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Warehouse.API.Models
{
    public partial class IssueSlip : DatabaseEntity
    {
        [Required]
        public long Id { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
