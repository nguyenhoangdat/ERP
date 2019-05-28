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
    public class FindDeletedReceiptItemsOnPageCommandHandler : IRequestHandler<FindDeletedReceiptItemsOnPageCommand, Page<Receipt.Item>>
    {
        public FindDeletedReceiptItemsOnPageCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Page<Receipt.Item>> Handle(FindDeletedReceiptItemsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Receipt.Item> items = this.DatabaseContext.ReceiptItems.Where(x => x.UtcMovedToBin != null);

            return new Page<Receipt.Item>(
                request.Page,
                request.ItemsPerPage,
                items.Count(),
                items.Skip(request.ItemsPerPage * (request.Page - 1)).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
