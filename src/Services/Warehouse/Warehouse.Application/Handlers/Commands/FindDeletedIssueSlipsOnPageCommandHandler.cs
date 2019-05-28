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
    public class FindDeletedIssueSlipsOnPageCommandHandler : IRequestHandler<FindDeletedIssueSlipsOnPageCommand, Page<IssueSlip>>
    {
        public FindDeletedIssueSlipsOnPageCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Page<IssueSlip>> Handle(FindDeletedIssueSlipsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<IssueSlip> issueSlips = this.DatabaseContext.IssueSlips.Where(x => x.UtcMovedToBin != null);

            return new Page<IssueSlip>(
                request.Page,
                request.ItemsPerPage,
                issueSlips.Count(),
                issueSlips.Skip(request.ItemsPerPage * (request.Page - 1)).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
