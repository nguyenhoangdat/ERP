using Restmium.ERP.BuildingBlocks.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public class Warehouse : DatabaseEntity
    {
        public Warehouse()
        {
            this.Sections = new HashSet<Section>();
        }
        public Warehouse(string name, Address address, bool isSystemEntity = false) : this()
        {
            this.Name = name;
            this.Address = address;
            this.IsSystemEntity = isSystemEntity;
        }
        public Warehouse(string name, Address address, ICollection<Section> sections, bool isSystemEntity = false) : this(name, address, isSystemEntity)
        {
            this.Sections = sections;
        }
        public Warehouse(int id, string name, Address address, bool isSystemEntity = false) : this(name, address, isSystemEntity)
        {
            this.Id = id;
        }
        public Warehouse(int id, string name, Address address, ICollection<Section> sections, bool isSystemEntity = false) : this(name, address, sections, isSystemEntity)
        {
            this.Id = id;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual Address Address { get; set; }

        [Required]
        public bool IsSystemEntity { get; }

        public virtual ICollection<Section> Sections { get; protected set; }
    }
}
