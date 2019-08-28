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
    public class MoveStockTakingToBinCommandHandler : IRequestHandler<MoveStockTakingToBinCommand, StockTaking>
    {
        public MoveStockTakingToBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<StockTaking> Handle(MoveStockTakingToBinCommand request, CancellationToken cancellationToken)
        {
            StockTaking stockTaking = this.DatabaseContext.StockTakings.FirstOrDefault(x => x.Id == request.StockTakingId);

            if (stockTaking == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.StockTaking_EntityNotFoundException, request.StockTakingId));
            }
            if (request.MovedToBinInCascade == false)
            {
                if (stockTaking.CanBeMovedToBin() == false)
                {
                    throw new EntityMoveToBinException(string.Format(Properties.Resources.StockTaking_EntityMoveToBinException, request.StockTakingId));
                }
            }            

            stockTaking.UtcMovedToBin = DateTime.UtcNow;
            stockTaking.MovedToBinInCascade = request.MovedToBinInCascade;

            foreach (StockTaking.Item item in stockTaking.Items.Where(x => x.UtcMovedToBin == null))
            {
                await this.Mediator.Send(new MoveStockTakingItemToBinCommand(item.StockTakingId, item.PositionId, true), cancellationToken);
            }

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return stockTaking;
        }
    }
}
