using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Linq;
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
            IssueSlip issueSlip = this.DatabaseContext.IssueSlips.FirstOrDefault(x => x.Id == request.IssueSlipId);

            if (issueSlip == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.IssueSlip_EntityNotFoundException, request.IssueSlipId));
            }

            issueSlip.UtcMovedToBin = null;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return issueSlip;
        }
    }
}
