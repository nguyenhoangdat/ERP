using Restmium.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.API.Models
{
    public class Position : DatabaseEntity
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int SectionId { get; set; }
        public virtual Section Section { get; set; }

        public int? WareId { get; set; }
        public virtual Ware Ware { get; set; }
        
        [Required]
        public int Rating { get; set; }

        public virtual ICollection<Movement> Movements { get; set; }
        public virtual ICollection<IssueSlip.Item> IssueSlipItems { get; set; }
        public virtual ICollection<StockTaking.Item> Items { get; set; }
    }
}
