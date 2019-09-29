using Restmium.ERP.BuildingBlocks.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public class Section : DatabaseEntity
    {
        protected Section()
        {
            this.Positions = new HashSet<Position>();
        }
        public Section(string name, int warehouseId, bool isSystemEntity = false) : this()
        {
            this.Name = name;
            this.WarehouseId = warehouseId;
            this.IsSystemEntity = isSystemEntity;
        }
        public Section(string name, int warehouseId, ICollection<Position> positions, bool isSystemEntity = false) : this(name, warehouseId, isSystemEntity)
        {
            this.Positions = positions;
        }
        public Section(string name, Warehouse warehouse, bool isSystemEntity = false) : this(name, warehouse.Id, isSystemEntity)
        { }
        public Section(string name, Warehouse warehouse, ICollection<Position> positions, bool isSystemEntity = false) : this(name, warehouse.Id, positions, isSystemEntity)
        { }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        [Required]
        public bool IsSystemEntity { get; }

        public virtual ICollection<Position> Positions { get; protected set; }
    }
}
