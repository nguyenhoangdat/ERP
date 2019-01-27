using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindIssueSlipByIdCommandHandler : IRequestHandler<FindIssueSlipByIdCommand, IssueSlip>
    {
        public FindIssueSlipByIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IssueSlip> Handle(FindIssueSlipByIdCommand request, CancellationToken cancellationToken)
        {
            IssueSlip issueSlip = await this.DatabaseContext.IssueSlips.FindAsync(new object[] { request.Model.IssueSlipId }, cancellationToken);

            if (issueSlip == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["IssueSlip_EntityNotFoundException"], request.Model.IssueSlipId));
            }

            return issueSlip;
        }
    }
}
