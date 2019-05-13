using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindReceiptItemByReceiptIdAndWareIdCommand : IRequest<Receipt.Item>
    {
        public FindReceiptItemByReceiptIdAndWareIdCommand(long receiptId, int wareId)
        {
            this.ReceiptId = receiptId;
            this.WareId = wareId;
        }

        public long ReceiptId { get; }
        public int WareId { get; }
    }
}
