using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.API.Models
{
    public class Address : Restmium.Models.Geo.Address
    {
        [Required]
        public int Id { get; set; }

        [JsonIgnore]
        public virtual Warehouse Warehouse { get; set; }
    }
}
