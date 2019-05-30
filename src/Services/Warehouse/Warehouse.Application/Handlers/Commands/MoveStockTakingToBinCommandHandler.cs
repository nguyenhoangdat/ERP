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
    public class MoveStockTakingToBinCommandHandler : IRequestHandler<MoveStockTakingToBinCommand, StockTaking>
    {
        public MoveStockTakingToBinCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<StockTaking> Handle(MoveStockTakingToBinCommand request, CancellationToken cancellationToken)
        {
            StockTaking stockTaking = await this.DatabaseContext.StockTakings.FindAsync(new object[] { request.StockTakingId }, cancellationToken);

            if (stockTaking == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["StockTaking_EntityNotFoundException"], request.StockTakingId));
            }

            stockTaking.UtcMovedToBin = DateTime.UtcNow;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return stockTaking;
        }
    }
}
