using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class StoreReceiptItemAtPositionCommand : IRequest<Position>
    {
        public StoreReceiptItemAtPositionCommand(long receiptId, int wareId, PositionCount positionCount)
        {
            this.ReceiptId = receiptId;
            this.WareId = wareId;
            this.PositionCount = positionCount;
        }

        public long ReceiptId { get; }
        public int WareId { get; }
        public PositionCount PositionCount { get; }
    }
}
