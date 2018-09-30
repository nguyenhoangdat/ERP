using Restmium.Models.Abstract;
using Restmium.Models.Geo;
using System.ComponentModel.DataAnnotations;

namespace Restmium.Models.Info
{
    public class InvoiceInfo : DatabaseEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual Address Address { get; set; }
    }
}
