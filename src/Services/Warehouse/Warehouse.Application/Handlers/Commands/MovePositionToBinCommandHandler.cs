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
    public class MovePositionToBinCommandHandler : IRequestHandler<MovePositionToBinCommand, Position>
    {
        public MovePositionToBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Position> Handle(MovePositionToBinCommand request, CancellationToken cancellationToken)
        {
            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == request.PositionId);

            if (position == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Position_EntityNotFoundException, request.PositionId));
            }
            if (request.MovedToBinInCascade == false)
            {
                if (position.CanBeMovedToBin() == false)
                {
                    throw new EntityMoveToBinException(string.Format(Properties.Resources.Position_EntityMoveToBinException, request.PositionId));
                }
            }            

            position.UtcMovedToBin = DateTime.UtcNow;
            position.MovedToBinInCascade = request.MovedToBinInCascade;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            foreach (Movement item in position.Movements.Where(x => x.UtcMovedToBin == null))
            {
                await this.Mediator.Send(new MoveMovementToBinCommand(item.Id, true), cancellationToken);
            }
            foreach (StockTaking.Item item in position.StockTakingItems.Where(x => x.UtcMovedToBin == null))
            {
                await this.Mediator.Send(new MoveStockTakingItemToBinCommand(item.StockTakingId, item.PositionId, true), cancellationToken);
            }
            foreach (Receipt.Item item in position.ReceiptItems.Where(x => x.UtcMovedToBin == null))
            {
                await this.Mediator.Send(new MoveReceiptItemToBinCommand(item.ReceiptId, item.PositionId, item.WareId, true), cancellationToken);
            }
            foreach (IssueSlip.Item item in position.IssueSlipItems.Where(x => x.UtcMovedToBin == null))
            {
                await this.Mediator.Send(new MoveIssueSlipItemToBinCommand(item.IssueSlipId, item.PositionId, item.WareId, true), cancellationToken);
            }

            return position;
        }
    }
}
