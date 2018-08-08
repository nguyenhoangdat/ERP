using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Warehouse.API.Models.StockTaking
{
    public class StockTaking
    {
        [Required]
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        [Required]
        public StockTakingArea Area { get; set; }

        public virtual ICollection<StockTakingItem> StockTakingItems { get; set; }
    }
}
