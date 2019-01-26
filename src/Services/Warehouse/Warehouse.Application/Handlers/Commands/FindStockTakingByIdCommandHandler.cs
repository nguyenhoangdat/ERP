using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindStockTakingByIdCommandHandler : IRequestHandler<FindStockTakingByIdCommand, StockTaking>
    {
        protected const string FindStockTakingByIdCommandHandler_EntityNotFoundException = "StockTaking(Id={0}) not found!";

        public FindStockTakingByIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<StockTaking> Handle(FindStockTakingByIdCommand request, CancellationToken cancellationToken)
        {
            StockTaking stockTaking = await this.DatabaseContext.StockTakings.FindAsync(request.Model.Id);

            if (stockTaking == null)
            {
                throw new EntityNotFoundException(string.Format(FindStockTakingByIdCommandHandler_EntityNotFoundException, request.Model.Id));
            }

            return stockTaking;
        }
    }
}
