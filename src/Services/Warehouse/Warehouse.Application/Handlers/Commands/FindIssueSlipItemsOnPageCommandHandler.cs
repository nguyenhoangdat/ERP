using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities = Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindIssueSlipItemsOnPageCommandHandler : IRequestHandler<FindIssueSlipItemsOnPageCommand, PageDTO<Entities.IssueSlip.Item>>
    {
        protected DatabaseContext DatabaseContext { get; set; }

        public FindIssueSlipItemsOnPageCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public async Task<PageDTO<Entities.IssueSlip.Item>> Handle(FindIssueSlipItemsOnPageCommand request, CancellationToken cancellationToken)
        {
            return new PageDTO<Entities.IssueSlip.Item>(
                request.Page,
                request.ItemsPerPage,
                this.DatabaseContext.IssueSlipItems.Skip(request.ItemsPerPage * --request.Page).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
