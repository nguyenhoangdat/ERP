using System;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Application.Models
{
    public partial class StockTakingDTO
    {
        /// <summary>
        /// DTO for <see cref="Domain.Entities.StockTaking.Item"/>
        /// </summary>
        public class StockTakingItemDTO
        {
            [Required]
            public int WareId { get; set; }
            [Required]
            public long PositionId { get; set; }
            [Required]
            public int StockTakingId { get; set; }
            [Required]
            public int CurrentStock { get; set; }

            [Required]
            public int CountedStock { get; set; }

            public DateTime? UtcCounted { get; set; }
        }
    }    
}
