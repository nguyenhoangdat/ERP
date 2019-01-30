using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindIssueSlipsByOrderIdCommandHandler : IRequestHandler<FindIssueSlipsByOrderIdCommand, IEnumerable<IssueSlip>>
    {
        public FindIssueSlipsByOrderIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IEnumerable<IssueSlip>> Handle(FindIssueSlipsByOrderIdCommand request, CancellationToken cancellationToken)
        {
            return this.DatabaseContext.IssueSlips.Where(x => x.OrderId == request.OrderId).AsEnumerable();
        }
    }
}
