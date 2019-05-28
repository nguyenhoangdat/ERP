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
    public class FindDeletedStockTakingItemsOnPageCommandHandler : IRequestHandler<FindDeletedStockTakingItemsOnPageCommand, Page<StockTaking.Item>>
    {
        public FindDeletedStockTakingItemsOnPageCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Page<StockTaking.Item>> Handle(FindDeletedStockTakingItemsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<StockTaking.Item> items = this.DatabaseContext.StockTakingItems.Where(x => x.UtcMovedToBin != null);

            return new Page<StockTaking.Item>(
                request.Page,
                request.ItemsPerPage,
                items.Count(),
                items.Skip(request.ItemsPerPage * (request.Page - 1)).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
