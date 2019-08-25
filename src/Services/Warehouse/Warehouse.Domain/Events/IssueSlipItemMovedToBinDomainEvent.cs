using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class IssueSlipItemMovedToBinDomainEvent : INotification
    {
        public IssueSlipItemMovedToBinDomainEvent(IssueSlip.Item item)
        {
            this.Item = item;
        }

        public IssueSlip.Item Item { get; }
    }
}
