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
    public class RestoreStockTakingFromBinCommandHandler : IRequestHandler<RestoreStockTakingFromBinCommand, StockTaking>
    {
        public RestoreStockTakingFromBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<StockTaking> Handle(RestoreStockTakingFromBinCommand request, CancellationToken cancellationToken)
        {
            StockTaking stockTaking = this.DatabaseContext.StockTakings.FirstOrDefault(x => x.Id == request.StockTakingId);

            if (stockTaking == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.StockTaking_EntityNotFoundException, request.StockTakingId));
            }
            if (stockTaking.CanBeRestoredFromBin() == false)
            {
                throw new EntityRestoreFromBinException(string.Format(Properties.Resources.StockTaking_EntityRestoreFromBinException, request.StockTakingId));
            }

            stockTaking.UtcMovedToBin = null;
            stockTaking.MovedToBinInCascade = false;

            foreach (StockTaking.Item item in stockTaking.Items.Where(x => x.MovedToBinInCascade))
            {
                await this.Mediator.Send(new RestoreStockTakingItemFromBinCommand(item.StockTakingId, item.PositionId), cancellationToken);
            }

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return stockTaking;
        }
    }
}
