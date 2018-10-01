using Restmium.Models.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.API.Models
{
    public partial class StockTaking
    {
        public class Item : DatabaseEntity
        {
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

            [NotMapped]
            public int Variance
            {
                get => this.CountedStock - this.CurrentStock;
            }
        }
    }        
}
