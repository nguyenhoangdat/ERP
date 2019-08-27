using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateStockTakingItemCountedStockCommandHandler : IRequestHandler<UpdateStockTakingItemCountedStockCommand, StockTaking.Item>
    {
        public UpdateStockTakingItemCountedStockCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<StockTaking.Item> Handle(UpdateStockTakingItemCountedStockCommand request, CancellationToken cancellationToken)
        {
            StockTaking.Item item = this.DatabaseContext.StockTakingItems.FirstOrDefault(x =>
                x.StockTakingId == request.StockTakingId &&
                x.PositionId == request.PositionId);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.StockTakingItem_EntityNotFoundException, request.StockTakingId, request.PositionId));
            }

            item.CountedStock = request.CountedStock;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new StockTakingItemUpdatedDomainEvent(item), cancellationToken);

            return item;
        }
    }
}
