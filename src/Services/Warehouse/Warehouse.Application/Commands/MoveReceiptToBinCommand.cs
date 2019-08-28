using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveReceiptToBinCommand : IRequest<Receipt>
    {
        public MoveReceiptToBinCommand(long receiptId, bool movedToBinInCascade)
        {
            this.ReceiptId = receiptId;
            this.MovedToBinInCascade = movedToBinInCascade;
        }

        public long ReceiptId { get; }
        public bool MovedToBinInCascade { get; }
    }
}
