using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RestoreReceiptItemFromBinCommand : IRequest<Receipt.Item>
    {
        public RestoreReceiptItemFromBinCommand(int wareId, long receiptId)
        {
            this.WareId = wareId;
            this.ReceiptId = receiptId;
        }

        public int WareId { get; }
        public long ReceiptId { get; }
    }
}
