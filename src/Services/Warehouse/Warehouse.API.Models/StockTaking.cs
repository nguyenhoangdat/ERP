using Restmium.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.API.Models
{
    public partial class StockTaking : DatabaseEntity
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; } //TODO: Rename -> Items
    }
}
