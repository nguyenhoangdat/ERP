using Restmium.Models.Geo;
using System.ComponentModel.DataAnnotations;

namespace Restmium.Models.Info
{
    public partial class ShippingInfo
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Address Address { get; set; }

        public string Company { get; set; }

        public string Note { get; set; }
        public string ContactPhone { get; set; }
    }
}
