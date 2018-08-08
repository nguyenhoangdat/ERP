using System.ComponentModel.DataAnnotations;

namespace Restmium.Models.Geo
{
    public class Address
    {
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string City { get; set; }
        public string POBox { get; set; } //Schránka na poště

        [Required]
        public string Country { get; set; }
    }
}
