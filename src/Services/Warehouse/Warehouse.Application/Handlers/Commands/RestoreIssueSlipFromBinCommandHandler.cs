using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RestoreIssueSlipFromBinCommandHandler : IRequestHandler<RestoreIssueSlipFromBinCommand, IssueSlip>
    {
        public RestoreIssueSlipFromBinCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IssueSlip> Handle(RestoreIssueSlipFromBinCommand request, CancellationToken cancellationToken)
        {
            IssueSlip issueSlip = await this.DatabaseContext.IssueSlips.FindAsync(new object[] { request.IssueSlipId }, cancellationToken);

            if (issueSlip == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["IssueSlip_EntityNotFoundException"], request.IssueSlipId));
            }

            issueSlip.UtcMovedToBin = null;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return issueSlip;
        }
    }
}
