using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveReceiptItemToBinCommand : IRequest<Receipt.Item>
    {
        public MoveReceiptItemToBinCommand(long receiptId, long positionId, int wareId, bool movedToBinInCascade)
        {
            this.ReceiptId = receiptId;
            this.PositionId = positionId;
            this.WareId = wareId;
            this.MovedToBinInCascade = movedToBinInCascade;
        }

        public long ReceiptId { get; }
        public long PositionId { get; }
        public int WareId { get; }
        public bool MovedToBinInCascade { get; }
    }
}
