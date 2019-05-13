using System;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Application.Models
{
    public partial class ReceiptDTO
    {
        /// <summary>
        /// DTO for <see cref="Domain.Entities.Receipt.Item"/>
        /// </summary>
        public class ItemDTO
        {
            [Required]
            public int WareId { get; set; }

            public long? PositionId { get; set; }

            [Required]
            public long ReceiptId { get; set; }

            public int CountOrdered { get; set; }
            public int? CountReceived { get; set; }

            public DateTime? UtcProcessed { get; set; }
        }
    }        
}
