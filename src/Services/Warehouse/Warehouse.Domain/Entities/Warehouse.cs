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
        public Warehouse(string name, Address address) : this()
        {
            this.Name = name;
            this.Address = address;
        }
        public Warehouse(string name, Address address, ICollection<Section> sections) : this(name, address)
        {
            this.Sections = sections;
        }
        public Warehouse(int id, string name, Address address) : this(name, address)
        {
            this.Id = id;
        }
        public Warehouse(int id, string name, Address address, ICollection<Section> sections) : this(name, address, sections)
        {
            this.Id = id;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual Address Address { get; set; }

        public virtual ICollection<Section> Sections { get; protected set; }
    }
}
