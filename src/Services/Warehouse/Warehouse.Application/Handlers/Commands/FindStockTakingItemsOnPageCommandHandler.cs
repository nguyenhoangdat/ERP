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
    public class FindStockTakingItemsOnPageCommandHandler : IRequestHandler<FindStockTakingItemsOnPageCommand, PageDto<StockTaking.Item>>
    {
        protected DatabaseContext DatabaseContext { get; }

        public FindStockTakingItemsOnPageCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public async Task<PageDto<StockTaking.Item>> Handle(FindStockTakingItemsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<StockTaking.Item> items = this.DatabaseContext.StockTakingItems.Where(x => x.UtcMovedToBin == null);

            return new PageDto<StockTaking.Item>(
                request.Page,
                request.ItemsPerPage,
                items.Count(),
                items.Skip(request.ItemsPerPage * --request.Page).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
