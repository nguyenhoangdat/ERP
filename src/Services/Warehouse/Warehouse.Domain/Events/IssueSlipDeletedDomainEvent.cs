using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class IssueSlipDeletedDomainEvent : INotification
    {
        public IssueSlipDeletedDomainEvent(IssueSlip issueSlip)
        {
            this.IssueSlip = issueSlip;
        }

        public IssueSlip IssueSlip { get; }
    }
}
