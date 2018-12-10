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
        }
        public Position(string name, double width, double height, double depth) : this()
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
        }
        public Position(string name, double width, double height, double depth, int rating) : this(name, width, height, depth)
        {
            this.Rating = rating;
        }
        public Position(string name, double width, double height, double depth, int sectionId, int rating) : this(name, width, height, depth, rating)
        {
            this.SectionId = sectionId;
        }
        public Position(string name, double width, double height, double depth, Section section, int rating) : this(name, width, height, depth, rating)
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
        public int SectionId { get; set; }
        public virtual Section Section { get; set; }
        
        [Required]
        public int Rating { get; set; }

        public virtual ICollection<Movement> Movements { get; protected set; }
        public virtual ICollection<IssueSlip.Item> IssueSlipItems { get; protected set; }
        public virtual ICollection<StockTaking.Item> StockTakingItems { get; protected set; }
    }
}
