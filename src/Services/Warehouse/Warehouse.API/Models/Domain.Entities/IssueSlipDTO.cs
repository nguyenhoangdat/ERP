using System;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities
{
    /// <summary>
    /// DTO for <see cref="Warehouse.Domain.Entities.IssueSlip"/>
    /// </summary>
    public partial class IssueSlipDTO
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long OrderId { get; set; }

        public DateTime UtcDispatchDate { get; set; }

        [Required]
        public DateTime UtcDeliveryDate { get; set; }
    }
}
