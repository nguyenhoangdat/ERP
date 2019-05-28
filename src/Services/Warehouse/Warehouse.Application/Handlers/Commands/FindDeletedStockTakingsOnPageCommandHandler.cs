using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindDeletedStockTakingsOnPageCommandHandler : IRequestHandler<FindDeletedStockTakingsOnPageCommand, Page<StockTaking>>
    {
        public FindDeletedStockTakingsOnPageCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Page<StockTaking>> Handle(FindDeletedStockTakingsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<StockTaking> stockTakings = this.DatabaseContext.StockTakings.Where(x => x.UtcMovedToBin != null);

            return new Page<StockTaking>(
                request.Page,
                request.ItemsPerPage,
                stockTakings.Count(),
                stockTakings.Skip(request.ItemsPerPage * (request.Page - 1)).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
