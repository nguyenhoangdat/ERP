using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RestoreReceiptFromBinCommand : IRequest<Receipt>
    {
        public RestoreReceiptFromBinCommand(long receiptId)
        {
            this.ReceiptId = receiptId;
        }

        public long ReceiptId { get; }
    }
}
