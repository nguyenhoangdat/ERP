using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Ordering.API.Models
{
    //TODO: Review properties in parent
    public class ShippingInfo// : Restmium.Models.Info.ShippingInfo
    {
        [Required]
        public long Id { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}