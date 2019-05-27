using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindPositionsForReceiptItemCommand : IRequest<IEnumerable<PositionCount>>
    {
        public FindPositionsForReceiptItemCommand(long receiptId, int wareId)
        {
            this.ReceiptId = receiptId;
            this.WareId = wareId;
        }

        public long ReceiptId { get; }
        public int WareId { get; }
    }
}
