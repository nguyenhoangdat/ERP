using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class AssignReceiptItemToPositionCommand : IRequest<Receipt.Item>
    {
        public AssignReceiptItemToPositionCommand(long receiptId, long positionId, int wareId)
        {
            this.ReceiptId = receiptId;
            this.PositionId = positionId;
            this.WareId = wareId;
        }

        public long ReceiptId { get; }
        public long PositionId { get; }
        public int WareId { get; }
    }
}
