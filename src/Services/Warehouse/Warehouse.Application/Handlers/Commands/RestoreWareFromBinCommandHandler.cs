using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RestoreWareFromBinCommandHandler : IRequestHandler<RestoreWareFromBinCommand, Ware>
    {
        public RestoreWareFromBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Ware> Handle(RestoreWareFromBinCommand request, CancellationToken cancellationToken)
        {
            Ware ware = this.DatabaseContext.Wares.FirstOrDefault(x => x.Id == request.WareId);

            if (ware == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Ware_EntityNotFoundException, request.WareId));
            }
            if (ware.CanBeRestoredFromBin() == false)
            {
                throw new EntityRestoreFromBinException(string.Format(Properties.Resources.Ware_EntityRestoreFromBinException, request.WareId));
            }

            ware.UtcMovedToBin = null;
            ware.MovedToBinInCascade = false;

            foreach (Movement item in ware.Movements.Where(x => x.MovedToBinInCascade && x.CanBeRestoredFromBin()))
            {
                await this.Mediator.Send(new RestoreMovementFromBinCommand(item.Id), cancellationToken);
            }
            foreach (StockTaking.Item item in ware.StockTakingItems.Where(x => x.MovedToBinInCascade && x.CanBeRestoredFromBin()))
            {
                await this.Mediator.Send(new RestoreStockTakingItemFromBinCommand(item.StockTakingId, item.PositionId), cancellationToken);
            }
            foreach (Receipt.Item item in ware.ReceiptItems.Where(x => x.MovedToBinInCascade && x.CanBeRestoredFromBin()))
            {
                await this.Mediator.Send(new RestoreReceiptItemFromBinCommand(item.ReceiptId, item.PositionId, item.WareId), cancellationToken);
            }
            foreach (IssueSlip.Item item in ware.IssueSlipItems.Where(x => x.MovedToBinInCascade && x.CanBeRestoredFromBin()))
            {
                await this.Mediator.Send(new RestoreIssueSlipItemFromBinCommand(item.IssueSlipId, item.PositionId, item.WareId), cancellationToken);
            }

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return ware;
        }
    }
}
