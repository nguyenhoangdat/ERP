using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RestoreStockTakingItemFromBinCommandHandler : IRequestHandler<RestoreStockTakingItemFromBinCommand, StockTaking.Item>
    {
        public RestoreStockTakingItemFromBinCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<StockTaking.Item> Handle(RestoreStockTakingItemFromBinCommand request, CancellationToken cancellationToken)
        {
            StockTaking.Item item = await this.DatabaseContext.StockTakingItems.FindAsync(new object[] { request.PositionId, request.StockTakingId }, cancellationToken);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["StockTakingItem_EntityNotFoundException"], request.StockTakingId, request.PositionId));
            }

            item.UtcMovedToBin = null;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return item;
        }
    }
}
