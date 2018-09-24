using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.API.Models
{
    public partial class StockTaking
    {
        [Required]
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        [Required]
        public StockTakingArea Area { get; set; }

        public virtual ICollection<Item> StockTakingItems { get; set; }
    }
}
