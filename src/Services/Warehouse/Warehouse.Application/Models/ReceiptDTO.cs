using System;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Application.Models
{
    /// <summary>
    /// DTO for <see cref="Domain.Entities.Receipt"/>
    /// </summary>
    public partial class ReceiptDTO
    {
        [Required]
        public long Id { get; set; }

        //TODO: Add supplier & transport details (contact, ...)

        public DateTime UtcExpected { get; set; }
        public DateTime? UtcReceived { get; set; }
    }
}
