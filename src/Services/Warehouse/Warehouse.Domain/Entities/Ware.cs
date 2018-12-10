using Restmium.ERP.BuildingBlocks.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public class Ware : DatabaseEntity
    {
        public Ware()
        {
            this.Movements = new HashSet<Movement>();
            this.IssueSlipItems = new HashSet<IssueSlip.Item>();
            this.StockTakingItems = new HashSet<StockTaking.Item>();
        }
        public Ware(int productId, string productName, double width, double height, double depth, double weight) : this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
            this.Weight = weight;
        }
        public Ware(int productId, string productName, double width, double height, double depth, double weight, ICollection<Movement> movements, ICollection<IssueSlip.Item> issueSlipItems, ICollection<StockTaking.Item> stockTakingItems) : this(productId, productName, width, height, depth, weight)
        {
            this.Movements = movements;
            this.IssueSlipItems = issueSlipItems;
            this.StockTakingItems = stockTakingItems;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public double Width { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public double Depth { get; set; }
        [Required]
        public double Weight { get; set; }

        public virtual ICollection<Movement> Movements { get; protected set; }
        public virtual ICollection<IssueSlip.Item> IssueSlipItems { get; protected set; }
        public virtual ICollection<StockTaking.Item> StockTakingItems { get; protected set; }
    }
}