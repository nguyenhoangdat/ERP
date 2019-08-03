using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateStockTakingCommandHandler : IRequestHandler<UpdateStockTakingCommand, StockTaking>
    {
        public UpdateStockTakingCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<StockTaking> Handle(UpdateStockTakingCommand request, CancellationToken cancellationToken)
        {
            StockTaking stockTaking = await this.DatabaseContext.StockTakings.FindAsync(new object[] { request.Id }, cancellationToken);

            if (stockTaking == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["StockTaking_EntityNotFoundException"], request.Id));
            }

            stockTaking.Name = request.Name;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new StockTakingUpdatedDomainEvent(stockTaking), cancellationToken);

            return stockTaking;
        }
    }
}
