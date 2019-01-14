using Restmium.ERP.Services.Warehouse.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public partial class IssueSlip
    {
        public class Item : WarePosition
        {
            public Item()
            {

            }
            private Item(int requestedUnits, int issuedUnits, int employeeId) : this()
            {
                this.RequestedUnits = requestedUnits;
                this.IssuedUnits = issuedUnits;
                this.EmployeeId = employeeId;
            }
            public Item(long issueSlipId, int wareId, long positionId, int requestedUnits, int issuedUnits, int employeeId) : this(requestedUnits, issuedUnits, employeeId)
            {
                this.IssueSlipId = issueSlipId;
                this.WareId = wareId;
                this.PositionId = positionId;
            }
            public Item(IssueSlip issueSlip, Ware ware, Position position, int requestedUnits, int issuedUnits, int employeeId) : this(requestedUnits, issuedUnits, employeeId)
            {
                this.IssueSlip = issueSlip;
                this.Ware = ware;
                this.Position = position;
            }

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
