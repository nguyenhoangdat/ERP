using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RestoreReceiptItemFromBinCommand : IRequest<Receipt.Item>
    {
        public RestoreReceiptItemFromBinCommand(long receiptId, long? positionId, int wareId)
        {
            this.WareId = wareId;
            this.PositionId = positionId;
            this.ReceiptId = receiptId;
        }

        public int WareId { get; }
        public long? PositionId { get; }
        public long ReceiptId { get; }
    }
}
