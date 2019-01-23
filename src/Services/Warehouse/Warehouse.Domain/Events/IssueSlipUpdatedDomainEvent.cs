using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class IssueSlipUpdatedDomainEvent : INotification
    {
        public IssueSlipUpdatedDomainEvent(IssueSlip issueSlip)
        {
            this.IssueSlip = issueSlip;
        }

        public IssueSlip IssueSlip { get; }
    }
}
