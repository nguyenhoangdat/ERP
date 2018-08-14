using Restmium.Models.Geo;
using System.ComponentModel.DataAnnotations;

namespace Restmium.Models.Info
{
    public class InvoiceInfo
    {
        [Required]
        public string Name { get; set; }

        public virtual Address Address { get; set; }
    }
}
