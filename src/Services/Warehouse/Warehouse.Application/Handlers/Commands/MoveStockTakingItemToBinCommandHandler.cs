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
    public class MoveStockTakingItemToBinCommandHandler : IRequestHandler<MoveStockTakingItemToBinCommand, StockTaking.Item>
    {
        public MoveStockTakingItemToBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<StockTaking.Item> Handle(MoveStockTakingItemToBinCommand request, CancellationToken cancellationToken)
        {
            StockTaking.Item item = this.DatabaseContext.StockTakingItems.FirstOrDefault(x =>
                x.StockTakingId == request.StockTakingId &&
                x.PositionId == request.PositionId);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.StockTakingItem_EntityNotFoundException, request.StockTakingId, request.PositionId));
            }
            if (request.MovedToBinInCascade == false)
            {
                if (item.CanBeMovedToBin() == false)
                {
                    throw new EntityMoveToBinException(string.Format(Properties.Resources.StockTakingItem_EntityMoveToBinException, request.StockTakingId, request.PositionId)); ;
                }
            }

            item.UtcMovedToBin = DateTime.UtcNow;
            item.MovedToBinInCascade = request.MovedToBinInCascade;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            if (item.StockTaking.CanBeMovedToBin())
            {
                await this.Mediator.Publish(new MoveStockTakingToBinCommand(item.StockTakingId, true), cancellationToken);
            }            

            return item;
        }
    }
}
