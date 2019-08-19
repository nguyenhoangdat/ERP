using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveReceiptItemToBinCommand : IRequest<Receipt.Item>
    {
        public MoveReceiptItemToBinCommand(int wareId, long? positionId, long receiptId)
        {
            this.WareId = wareId;
            this.PositionId = positionId;
            this.ReceiptId = receiptId;
        }

        public int WareId { get; set; }
        public long? PositionId { get; }
        public long ReceiptId { get; set; }
    }
}
