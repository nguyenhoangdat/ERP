using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RestoreReceiptFromBinCommandHandler : IRequestHandler<RestoreReceiptFromBinCommand, Receipt>
    {
        public RestoreReceiptFromBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Receipt> Handle(RestoreReceiptFromBinCommand request, CancellationToken cancellationToken)
        {
            Receipt receipt = this.DatabaseContext.Receipts.FirstOrDefault(x => x.Id == request.ReceiptId);

            if (receipt == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Receipt_EntityNotFoundException, request.ReceiptId));
            }
            if (receipt.CanBeRestoredFromBin() == false)
            {
                throw new EntityRestoreFromBinException(string.Format(Properties.Resources.Receipt_EntityRestoreFromBinException, request.ReceiptId));
            }

            receipt.UtcMovedToBin = null;
            receipt.MovedToBinInCascade = false;

            foreach (Receipt.Item item in receipt.Items.Where(x => x.MovedToBinInCascade == false))
            {
                await this.Mediator.Send(new RestoreReceiptItemFromBinCommand(item.ReceiptId, item.PositionId, item.WareId), cancellationToken);
            }

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return receipt;
        }
    }
}
