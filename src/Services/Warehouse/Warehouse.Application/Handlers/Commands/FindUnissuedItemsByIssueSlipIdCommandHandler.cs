using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindUnissuedIssueSlipItemsByIssueSlipIdCommandHandler : IRequestHandler<FindUnissuedIssueSlipItemsByIssueSlipIdCommand, IEnumerable<IssueSlip.Item>>
    {
        public FindUnissuedIssueSlipItemsByIssueSlipIdCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IEnumerable<IssueSlip.Item>> Handle(FindUnissuedIssueSlipItemsByIssueSlipIdCommand request, CancellationToken cancellationToken)
        {
            IssueSlip issueSlip = this.DatabaseContext.IssueSlips.FirstOrDefault(x => x.Id == request.IssueSlipId);
            if (issueSlip == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.IssueSlip_EntityNotFoundException, request.IssueSlipId));
            }

            return this.DatabaseContext.IssueSlipItems.Where(x =>
                x.IssueSlipId == request.IssueSlipId &&
                x.IssuedUnits < x.RequestedUnits);
        }
    }
}
