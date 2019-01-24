using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class IssueSlipItemUpdatedDomainEvent : INotification
    {
        public IssueSlipItemUpdatedDomainEvent(IssueSlip.Item item)
        {
            this.Item = item;
        }

        public IssueSlip.Item Item { get; }
    }
}
