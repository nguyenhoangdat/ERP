using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindDeletedReceiptsOnPageCommandHandler : IRequestHandler<FindDeletedReceiptsOnPageCommand, Page<Receipt>>
    {
        public FindDeletedReceiptsOnPageCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Page<Receipt>> Handle(FindDeletedReceiptsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Receipt> receipts = this.DatabaseContext.Receipts.Where(x => x.UtcMovedToBin != null);

            return new Page<Receipt>(
                request.Page,
                request.ItemsPerPage,
                receipts.Count(),
                receipts.Skip(request.ItemsPerPage * (request.Page - 1)).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
