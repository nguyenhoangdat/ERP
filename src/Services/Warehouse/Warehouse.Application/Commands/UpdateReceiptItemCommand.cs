using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateReceiptItemCommand : IRequest<Receipt.Item>
    {
        public UpdateReceiptItemCommand(int wareId, long? positionId, long receiptId, int countOrdered, int countReceived, DateTime? utcProcessed)
        {
            this.WareId = wareId;
            this.PositionId = positionId;
            this.ReceiptId = receiptId;
            this.CountOrdered = countOrdered;
            this.CountReceived = countReceived;
            this.UtcProcessed = utcProcessed;
        }

        public int WareId { get; }
        public long? PositionId { get; }
        public long ReceiptId { get; }

        public int CountOrdered { get; }
        public int CountReceived { get; }
        public DateTime? UtcProcessed { get; }
    }
}
