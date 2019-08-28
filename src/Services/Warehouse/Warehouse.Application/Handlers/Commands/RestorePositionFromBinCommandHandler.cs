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
    public class RestorePositionFromBinCommandHandler : IRequestHandler<RestorePositionFromBinCommand, Position>
    {
        public RestorePositionFromBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Position> Handle(RestorePositionFromBinCommand request, CancellationToken cancellationToken)
        {
            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == request.PositionId);

            if (position == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Position_EntityNotFoundException, request.PositionId));
            }

            position.UtcMovedToBin = null;
            position.MovedToBinInCascade = false;

            foreach (Movement item in position.Movements.Where(x => x.MovedToBinInCascade && x.CanBeRestoredFromBin()))
            {
                await this.Mediator.Send(new RestoreMovementFromBinCommand(item.Id), cancellationToken);
            }
            foreach (StockTaking.Item item in position.StockTakingItems.Where(x => x.MovedToBinInCascade && x.CanBeRestoredFromBin()))
            {
                await this.Mediator.Send(new RestoreStockTakingItemFromBinCommand(item.StockTakingId, item.PositionId), cancellationToken);
            }
            foreach (Receipt.Item item in position.ReceiptItems.Where(x => x.MovedToBinInCascade && x.CanBeRestoredFromBin()))
            {
                await this.Mediator.Send(new RestoreReceiptItemFromBinCommand(item.ReceiptId, item.PositionId, item.WareId), cancellationToken);
            }
            foreach (IssueSlip.Item item in position.IssueSlipItems.Where(x => x.MovedToBinInCascade && x.CanBeRestoredFromBin()))
            {
                await this.Mediator.Send(new RestoreIssueSlipItemFromBinCommand(item.IssueSlipId, item.PositionId, item.WareId), cancellationToken);
            }

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return position;
        }
    }
}
