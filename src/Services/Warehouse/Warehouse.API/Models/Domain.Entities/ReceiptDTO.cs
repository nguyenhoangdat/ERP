using System;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities
{
    /// <summary>
    /// DTO for <see cref="Warehouse.Domain.Entities.Receipt"/>
    /// </summary>
    public partial class ReceiptDTO
    {
        [Required]
        public long Id { get; set; }

        //TODO: 2019.2 Add supplier & transport details (contact, ...)

        public DateTime UtcExpected { get; set; }
        public DateTime? UtcReceived { get; set; }
    }
}
