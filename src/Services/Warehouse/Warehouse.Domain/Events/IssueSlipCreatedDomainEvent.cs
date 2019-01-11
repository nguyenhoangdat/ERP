using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class IssueSlipCreatedDomainEvent : INotification
    {
        public IssueSlipCreatedDomainEvent()
        {
        }

        public IssueSlipCreatedDomainEvent(StockTaking issueSlip) : this()
        {
            this.IssueSlip = issueSlip;
        }

        public StockTaking IssueSlip { get; }
    }
}
