using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class StoreUnitsForReceiptItemAtPositionCommand : IRequest<Receipt.Item>
    {
        public StoreUnitsForReceiptItemAtPositionCommand(long receiptId, long positionId, int wareId, int count)
        {
            this.ReceiptId = receiptId;

            if (positionId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(positionId));
            }
            this.PositionId = positionId;
            this.WareId = wareId;
            this.Count = count;
        }

        public long ReceiptId { get; }
        public long PositionId { get; }
        public int WareId { get; }
        public int Count { get; }
    }
}
