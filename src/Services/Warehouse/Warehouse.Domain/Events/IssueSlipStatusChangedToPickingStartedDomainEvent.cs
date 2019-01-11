using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class IssueSlipStatusChangedToPickingStartedDomainEvent
    {
        public IssueSlipStatusChangedToPickingStartedDomainEvent(StockTaking issueSlip)
        {
            this.IssueSlip = issueSlip;
        }

        public StockTaking IssueSlip { get; }
    }
}
