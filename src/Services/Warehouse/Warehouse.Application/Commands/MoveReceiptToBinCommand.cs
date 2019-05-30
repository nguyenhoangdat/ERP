using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveReceiptToBinCommand : IRequest<Receipt>
    {
        public MoveReceiptToBinCommand(long receiptId)
        {
            this.ReceiptId = receiptId;
        }

        public long ReceiptId { get; }
    }
}
