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
    public class FindReceiptItemsOnPageCommandHandler : IRequestHandler<FindReceiptItemsOnPageCommand, PageDto<Receipt.Item>>
    {
        protected DatabaseContext DatabaseContext { get; set; }

        public FindReceiptItemsOnPageCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public async Task<PageDto<Receipt.Item>> Handle(FindReceiptItemsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Receipt.Item> items = this.DatabaseContext.ReceiptItems.Where(x => x.UtcMovedToBin == null);

            return new PageDto<Receipt.Item>(
                request.Page,
                request.ItemsPerPage,
                items.Count(),
                items.Skip(request.ItemsPerPage * --request.Page).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
