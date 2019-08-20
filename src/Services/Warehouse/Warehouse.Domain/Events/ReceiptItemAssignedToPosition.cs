using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class ReceiptItemAssignedToPosition : INotification
    {
        public ReceiptItemAssignedToPosition(Receipt.Item item)
        {
            this.Item = item;
        }

        public Receipt.Item Item { get; }
    }
}
