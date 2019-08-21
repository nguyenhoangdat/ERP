using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class ReceiptItemAssignedToPositionDomainEvent : INotification
    {
        public ReceiptItemAssignedToPositionDomainEvent(Receipt.Item item)
        {
            this.Item = item;
        }

        public Receipt.Item Item { get; }
    }
}
