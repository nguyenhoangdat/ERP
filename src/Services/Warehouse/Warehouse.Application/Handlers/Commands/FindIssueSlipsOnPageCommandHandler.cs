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
    public class FindIssueSlipsOnPageCommandHandler : IRequestHandler<FindIssueSlipsOnPageCommand, PageDTO<IssueSlip>>
    {
        protected DatabaseContext DatabaseContext { get; set; }

        public FindIssueSlipsOnPageCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public async Task<PageDTO<IssueSlip>> Handle(FindIssueSlipsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<IssueSlip> issueSlips = this.DatabaseContext.IssueSlips.Where(x => x.UtcMovedToBin == null);

            return new PageDTO<IssueSlip>(
                request.Page,
                request.ItemsPerPage,
                issueSlips.Count(),
                issueSlips.Skip(request.ItemsPerPage * --request.Page).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
