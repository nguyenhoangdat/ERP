using Restmium.ERP.BuildingBlocks.Common.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public partial class StockTaking
    {
        public class Item : DatabaseEntity
        {
            public Item()
            {

            }
            public Item(int stockTakingId, int wareId, long positionId, int currentStock, int countedStock) : this()
            {
                this.StockTakingId = stockTakingId;
                this.WareId = wareId;
                this.PositionId = positionId;
                this.CurrentStock = currentStock;
                this.CountedStock = countedStock;
            }
            public Item(int stockTakingId, int wareId, long positionId, int currentStock, int countedStock, DateTime utcCounted) : this(stockTakingId, wareId, positionId, currentStock, countedStock)
            {
                this.UtcCounted = utcCounted;
            }

            [Required]
            public long Id { get; set; }

            [Required]
            public int StockTakingId { get; set; }
            public virtual StockTaking StockTaking { get; set; }

            [Required]
            public int WareId { get; set; }
            public virtual Ware Ware { get; set; }

            [Required]
            public long PositionId { get; set; }
            public virtual Position Position { get; set; }

            [Required]
            public int CurrentStock { get; set; }

            [Required]
            public int CountedStock { get; set; }

            public DateTime? UtcCounted { get; set; }
        }
    }        
}
