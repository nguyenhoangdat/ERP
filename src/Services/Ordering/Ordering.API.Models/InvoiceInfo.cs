using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Ordering.API.Models
{
    //TODO: Review properties in parent
    public class InvoiceInfo //: Restmium.Models.Info.InvoiceInfo
    {
        [Required]
        public long Id { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}