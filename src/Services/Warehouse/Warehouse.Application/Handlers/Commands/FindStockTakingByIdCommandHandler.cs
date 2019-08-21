using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindStockTakingByIdCommandHandler : IRequestHandler<FindStockTakingByIdCommand, StockTaking>
    {
        public FindStockTakingByIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<StockTaking> Handle(FindStockTakingByIdCommand request, CancellationToken cancellationToken)
        {
            StockTaking stockTaking = this.DatabaseContext.StockTakings.FirstOrDefault(x => x.Id == request.Id);

            if (stockTaking == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["StockTaking_EntityNotFoundException"], request.Id));
            }

            return stockTaking;
        }
    }
}
