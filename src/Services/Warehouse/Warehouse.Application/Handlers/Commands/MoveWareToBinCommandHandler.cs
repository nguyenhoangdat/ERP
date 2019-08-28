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
    public class MoveWareToBinCommandHandler : IRequestHandler<MoveWareToBinCommand, Ware>
    {
        public MoveWareToBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Ware> Handle(MoveWareToBinCommand request, CancellationToken cancellationToken)
        {
            Ware ware = this.DatabaseContext.Wares.FirstOrDefault(x => x.Id == request.WareId);

            if (ware == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Ware_EntityNotFoundException, request.WareId));
            }
            if (request.MovedToBinInCascade == false)
            {
                if (ware.CanBeMovedToBin() == false)
                {
                    throw new EntityMoveToBinException(string.Format(Properties.Resources.Ware_EntityMoveToBinException, request.WareId));
                }
            }

            ware.UtcMovedToBin = DateTime.UtcNow;
            ware.MovedToBinInCascade = request.MovedToBinInCascade;

            foreach (Movement item in ware.Movements.Where(x => x.UtcMovedToBin == null))
            {
                await this.Mediator.Send(new MoveMovementToBinCommand(item.Id, true), cancellationToken);
            }
            foreach (StockTaking.Item item in ware.StockTakingItems.Where(x => x.UtcMovedToBin == null))
            {
                await this.Mediator.Send(new MoveStockTakingItemToBinCommand(item.StockTakingId, item.PositionId, true), cancellationToken);
            }
            foreach (Receipt.Item item in ware.ReceiptItems.Where(x => x.UtcMovedToBin == null))
            {
                await this.Mediator.Send(new MoveReceiptItemToBinCommand(item.ReceiptId, item.PositionId, item.WareId, true), cancellationToken);
            }
            foreach (IssueSlip.Item item in ware.IssueSlipItems.Where(x => x.UtcMovedToBin == null))
            {
                await this.Mediator.Send(new MoveIssueSlipItemToBinCommand(item.IssueSlipId, item.PositionId, item.WareId, true), cancellationToken);
            }

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return ware;
        }
    }
}
