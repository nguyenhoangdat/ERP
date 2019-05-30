using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveReceiptItemToBinCommand : IRequest<Receipt.Item>
    {
        public MoveReceiptItemToBinCommand(int wareId, long receiptId)
        {
            this.WareId = wareId;
            this.ReceiptId = receiptId;
        }

        public int WareId { get; set; }
        public long ReceiptId { get; set; }
    }
}
