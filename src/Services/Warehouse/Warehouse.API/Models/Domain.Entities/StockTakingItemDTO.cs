using System;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities
{
    public partial class StockTakingDTO
    {
        /// <summary>
        /// DTO for <see cref="Warehouse.Domain.Entities.StockTaking.Item"/>
        /// </summary>
        public class ItemDTO
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
