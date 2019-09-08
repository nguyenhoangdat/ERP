using Restmium.ERP.BuildingBlocks.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public partial class IssueSlip
    {
        public class Item : DatabaseEntity
        {
            protected Item()
            {
                this.PositionId = 1;
            }
            protected Item(int requestedUnits, int issuedUnits) : this()
            {
                this.RequestedUnits = requestedUnits;
                this.IssuedUnits = issuedUnits;
            }
            public Item(long issueSlipId, int wareId, long positionId, int requestedUnits, int issuedUnits) : this(requestedUnits, issuedUnits)
            {
                this.IssueSlipId = issueSlipId;
                this.WareId = wareId;
                this.PositionId = positionId;
            }

            [Required]
            public int WareId { get; set; }
            public virtual Ware Ware { get; set; }

            [Required]
            public long PositionId { get; set; }
            public virtual Position Position { get; set; }

            [Required]
            public long IssueSlipId { get; set; }
            public virtual IssueSlip IssueSlip { get; set; }

            [Required]
            public int RequestedUnits { get; set; } //Počet vyžádaných jednotek
            [Required]
            public int IssuedUnits { get; set; } //Počet vydaných jednotek
        }
    }    
}
