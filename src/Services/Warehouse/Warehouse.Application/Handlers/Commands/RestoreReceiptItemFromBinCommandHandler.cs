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
    public class RestoreReceiptItemFromBinCommandHandler : IRequestHandler<RestoreReceiptItemFromBinCommand, Receipt.Item>
    {
        public RestoreReceiptItemFromBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Receipt.Item> Handle(RestoreReceiptItemFromBinCommand request, CancellationToken cancellationToken)
        {
            Receipt.Item item = this.DatabaseContext.ReceiptItems.FirstOrDefault(x =>
                x.ReceiptId == request.ReceiptId &&
                x.PositionId == request.PositionId &&
                x.WareId == request.WareId);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.ReceiptItem_EntityNotFoundException, request.ReceiptId, request.PositionId, request.WareId));
            }
            if (item.Receipt.CanBeRestoredFromBin())
            {
                await this.Mediator.Send(new RestoreReceiptFromBinCommand(item.ReceiptId), cancellationToken);
            }
            if (item.CanBeRestoredFromBin() == false)
            {
                throw new EntityRestoreFromBinException(string.Format(Properties.Resources.ReceiptItem_EntityRestoreFromBinException, request.ReceiptId, request.PositionId, request.WareId));
            }

            item.UtcMovedToBin = null;
            item.MovedToBinInCascade = false;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return item;
        }
    }
}
