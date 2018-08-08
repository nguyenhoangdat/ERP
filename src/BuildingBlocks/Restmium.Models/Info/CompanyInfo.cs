using System.ComponentModel.DataAnnotations;

namespace Restmium.Models.Info
{
    public partial class CompanyInfo
    {
        [Required]
        public string CompanyId { get; set; }
        public string VATId { get; set; }
        public string BankAccount { get; set; }
        public string SpecificSymbol { get; set; }
    }
}
