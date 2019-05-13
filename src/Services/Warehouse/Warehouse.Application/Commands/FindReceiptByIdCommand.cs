using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindReceiptByIdCommand : IRequest<Receipt>
    {
        public FindReceiptByIdCommand(long receiptId)
        {
            this.ReceiptId = receiptId;
        }

        public long ReceiptId { get; }
    }
}
