using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class IssueSlipStatusChangedToPickingStartedDomainEvent
    {
        public IssueSlipStatusChangedToPickingStartedDomainEvent(IssueSlip issueSlip)
        {
            this.IssueSlip = issueSlip;
        }

        public IssueSlip IssueSlip { get; }
    }
}
