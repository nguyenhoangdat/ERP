using Restmium.Models.Geo;
using System.ComponentModel.DataAnnotations;

namespace Restmium.Models.Info
{

    public partial class InvoiceInfo
    {
        [Required]
        public string Name { get; set; }

        public Address Address { get; set; }
    }
}
