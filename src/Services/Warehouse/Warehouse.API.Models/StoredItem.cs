using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Warehouse.API.Models.StockTaking;

namespace Warehouse.API.Models
{
    public class StoredItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<Movement> Movements { get; set; }
        public virtual ICollection<StockTakingItem> StockTakingItems { get; set; }
    }
}