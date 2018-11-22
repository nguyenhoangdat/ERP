using Newtonsoft.Json;
using Restmium.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.API.Models
{
    public class Address : DatabaseEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string ZipCode { get; set; }

        [JsonIgnore]
        public virtual Warehouse Warehouse { get; set; }
    }
}
