using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class DeleteStockTakingCommandHandler : IRequestHandler<DeleteStockTakingCommand, StockTaking>
    {
        public DeleteStockTakingCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<StockTaking> Handle(DeleteStockTakingCommand request, CancellationToken cancellationToken)
        {
            StockTaking stockTaking = await this.DatabaseContext.StockTakings.FindAsync(new object[] { request.Id }, cancellationToken);
            if (stockTaking == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["StockTaking_EntityNotFoundException"], request.Id));
            }

            stockTaking = this.DatabaseContext.StockTakings.Remove(stockTaking).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return stockTaking;
        }
    }
}
