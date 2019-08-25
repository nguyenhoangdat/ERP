using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class IssueSlipItemRestoredFromBinDomainEvent : INotification
    {
        public IssueSlipItemRestoredFromBinDomainEvent(IssueSlip.Item item)
        {
            this.Item = item;
        }

        public IssueSlip.Item Item { get; }
    }
}
