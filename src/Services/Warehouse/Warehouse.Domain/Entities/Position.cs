using Restmium.ERP.BuildingBlocks.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public class Position : DatabaseEntity
    {
        public Position()
        {
            this.Movements = new HashSet<Movement>();
            this.IssueSlipItems = new HashSet<IssueSlip.Item>();
            this.StockTakingItems = new HashSet<StockTaking.Item>();
            this.ReceiptItems = new HashSet<Receipt.Item>();
        }
        public Position(string name, double width, double height, double depth, double maxWeight) : this()
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
            this.MaxWeight = maxWeight;
        }
        public Position(string name, double width, double height, double depth, double maxWeight, int rating) : this(name, width, height, depth, maxWeight)
        {
            this.Rating = rating;
        }
        public Position(string name, double width, double height, double depth, double maxWeight, int sectionId, int rating) : this(name, width, height, depth, maxWeight, rating)
        {
            this.SectionId = sectionId;
        }
        public Position(string name, double width, double height, double depth, double maxWeight, Section section, int rating) : this(name, width, height, depth, maxWeight, rating)
        {
            this.Section = section;
        }

        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public double Width { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public double Depth { get; set; }
        [Required]
        public double MaxWeight { get; set; }
        public int ReservedUnits { get; set; }

        [Required]
        public int SectionId { get; set; }
        public virtual Section Section { get; set; }
        
        [Required]
        public int Rating { get; set; }

        public virtual ICollection<Movement> Movements { get; protected set; }
        public virtual ICollection<IssueSlip.Item> IssueSlipItems { get; protected set; }
        public virtual ICollection<StockTaking.Item> StockTakingItems { get; protected set; }
        public virtual ICollection<Receipt.Item> ReceiptItems { get; protected set; }
    }
}
