using Restmium.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.API.Models
{
    public class Warehouse : DatabaseEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
    }
}
