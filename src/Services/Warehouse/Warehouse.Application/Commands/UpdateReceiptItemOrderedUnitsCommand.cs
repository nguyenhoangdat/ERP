using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateReceiptItemOrderedUnitsCommand : IRequest<Receipt.Item>
    {
        public UpdateReceiptItemOrderedUnitsCommand(long receiptId, long? positionId, int wareId, int orderedUnits)
        {
            this.ReceiptId = receiptId;
            this.PositionId = positionId;
            this.WareId = wareId;
            this.OrderedUnits = orderedUnits;
        }

        public long ReceiptId { get; }
        public long? PositionId { get; }
        public int WareId { get; }
        public int OrderedUnits { get; }
    }
}
