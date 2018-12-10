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
        public Warehouse(string name, int addressId) : this()
        {
            this.Name = name;
            this.AddressId = addressId;
        }
        public Warehouse(string name, Address address) : this()
        {
            this.Name = name;
            this.Address = address;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<Section> Sections { get; protected set; }
    }
}
