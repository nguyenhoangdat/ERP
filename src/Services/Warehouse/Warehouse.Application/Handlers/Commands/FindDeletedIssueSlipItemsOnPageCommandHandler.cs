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
    public class FindDeletedIssueSlipItemsOnPageCommandHandler : IRequestHandler<FindDeletedIssueSlipItemsOnPageCommand, Page<IssueSlip.Item>>
    {
        public FindDeletedIssueSlipItemsOnPageCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Page<IssueSlip.Item>> Handle(FindDeletedIssueSlipItemsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<IssueSlip.Item> items = this.DatabaseContext.IssueSlipItems.Where(x => x.UtcMovedToBin != null);

            return new Page<IssueSlip.Item>(
                request.Page,
                request.ItemsPerPage,
                items.Count(),
                items.Skip(request.ItemsPerPage * (request.Page - 1)).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
