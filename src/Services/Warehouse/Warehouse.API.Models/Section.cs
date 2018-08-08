using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.API.Models
{
    public class Section
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }
}
