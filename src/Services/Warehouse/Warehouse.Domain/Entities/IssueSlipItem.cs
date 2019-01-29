using Restmium.ERP.BuildingBlocks.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public partial class IssueSlip
    {
        public class Item : DatabaseEntity
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
            public int WareId { get; set; }
            public virtual Ware Ware { get; set; }

            [Required]
            public long PositionId { get; set; }
            public virtual Position Position { get; set; }

            /// <summary>
            /// FK to Employees.API
            /// </summary>
            [Required]
            public int EmployeeId { get; set; }

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
