using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class MoveUnprocessedStockTakingItemsBetweenPositionsCommandHandler : IRequestHandler<MoveUnprocessedStockTakingItemsBetweenPositionsCommand, IEnumerable<StockTaking.Item>>
    {
        public MoveUnprocessedStockTakingItemsBetweenPositionsCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IEnumerable<StockTaking.Item>> Handle(MoveUnprocessedStockTakingItemsBetweenPositionsCommand request, CancellationToken cancellationToken)
        {
            List<StockTaking.Item> dbEntities = this.DatabaseContext.StockTakingItems.Where(x => x.PositionId == request.FromPositionId && x.UtcCounted == null).ToList();
            List<StockTaking.Item> output = new List<StockTaking.Item>(dbEntities.Count * 2);

            foreach (StockTaking.Item item in dbEntities)
            {
                StockTaking.Item entity = this.DatabaseContext.StockTakingItems.FirstOrDefault(x => x.PositionId == request.ToPositionId && x.UtcCounted == null);
                if (entity != null)
                {
                    output.Add(await this.Mediator.Send(new UpdateStockTakingItemCurrentStockCommand(entity.StockTakingId, entity.PositionId, entity.CurrentStock + item.CurrentStock), cancellationToken));
                }

                output.Add(await this.Mediator.Send(new MoveStockTakingItemToBinCommand(item.PositionId, item.StockTakingId), cancellationToken));
            }

            return output;
        }
    }
}
