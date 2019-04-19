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
    public class FindStockTakingsOnPageCommandHandler : IRequestHandler<FindStockTakingsOnPageCommand, PageDto<StockTaking>>
    {
        protected DatabaseContext DatabaseContext { get; }

        public FindStockTakingsOnPageCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public async Task<PageDto<StockTaking>> Handle(FindStockTakingsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<StockTaking> stockTakings = this.DatabaseContext.StockTakings.Where(x => x.UtcMovedToBin == null);

            return new PageDto<StockTaking>(
                request.Page,
                request.ItemsPerPage,
                stockTakings.Count(),
                stockTakings.Skip(request.ItemsPerPage * --request.Page).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
