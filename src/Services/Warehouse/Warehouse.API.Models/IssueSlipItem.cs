using Restmium.Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.API.Models
{
    public partial class IssueSlip
    {
        public class Item : DatabaseEntity
        {
            [Required]
            public long IssueSlipId { get; set; }
            public virtual IssueSlip IssueSlip { get; set; }

            [Required]
            public int WareId { get; set; }
            public virtual Ware Ware { get; set; }

            [Required]
            public int RequestedUnits { get; set; } //Počet vyžádaných jednotek
            [Required]
            public int IssuedUnits { get; set; } //Počet vydaných jednotek

            [Required]
            public long PositionId { get; set; }
            public Position Position { get; set; }

            //[Required]
            //public DateTime Issued { get; set; } //Datum a čas vydání zboží z pozice
        }
    }    
}
