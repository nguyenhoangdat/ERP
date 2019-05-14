using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Application.Models
{
    public partial class IssueSlipDTO
    {
        /// <summary>
        /// DTO for <see cref="Domain.Entities.IssueSlip.Item"/>
        /// </summary>
        public class ItemDTO
        {
            [Required]
            public int WareId { get; set; }

            [Required]
            public int PositionId { get; set; }

            [Required]
            public long IssueSlipId { get; set; }

            [Required]
            public int RequestedUnits { get; set; }
            [Required]
            public int IssuedUnits { get; set; }
        }
    }
}
