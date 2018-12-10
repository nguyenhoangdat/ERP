using Restmium.ERP.BuildingBlocks.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public class Section : DatabaseEntity
    {
        public Section()
        {
            this.Positions = new HashSet<Position>();
        }
        public Section(string name, int warehouseId) : this()
        {
            this.Name = name;
            this.WarehouseId = warehouseId;
        }
        public Section(string name, Warehouse warehouse) : this()
        {
            this.Name = name;
            this.Warehouse = warehouse;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        public virtual ICollection<Position> Positions { get; protected set; }
    }
}
