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
    public class FindStockTakingsOnPageCommandHandler : IRequestHandler<FindStockTakingsOnPageCommand, PageDTO<StockTaking>>
    {
        protected DatabaseContext DatabaseContext { get; }

        public FindStockTakingsOnPageCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public async Task<PageDTO<StockTaking>> Handle(FindStockTakingsOnPageCommand request, CancellationToken cancellationToken)
        {
            return new PageDTO<StockTaking>(
                request.Page,
                request.ItemsPerPage,
                this.DatabaseContext.StockTakings.Skip(request.ItemsPerPage * --request.Page).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
