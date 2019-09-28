using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class AssignReceiptItemToPositionCommand : IRequest<Receipt.Item>
    {
        public AssignReceiptItemToPositionCommand(long receiptId, long positionId, int wareId)
        {
            this.ReceiptId = receiptId;

            if (positionId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(positionId));
            }
            this.PositionId = positionId;
            this.WareId = wareId;
        }

        public long ReceiptId { get; }
        public long PositionId { get; }
        public int WareId { get; }
    }
}
