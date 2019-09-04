using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateReceiptItemCommand : IRequest<Receipt.Item>
    {
        public CreateReceiptItemCommand(int wareId, long positionId, long receiptId, int countOrdered, int countReceived)
        {
            this.ReceiptId = receiptId;
            this.PositionId = positionId;
            this.WareId = wareId;
            this.CountOrdered = countOrdered;
            this.CountReceived = countReceived;
        }

        public long ReceiptId { get; }
        public long PositionId { get; }
        public int WareId { get; }
        public int CountOrdered { get; }
        public int CountReceived { get; }
    }
}
