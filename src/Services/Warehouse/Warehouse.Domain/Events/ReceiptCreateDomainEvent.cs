using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class ReceiptCreateDomainEvent : INotification
    {
        public ReceiptCreateDomainEvent(Receipt receipt)
        {
            this.Receipt = receipt;
        }

        public Receipt Receipt { get; }
    }
}
