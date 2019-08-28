using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RestoreIssueSlipFromBinCommandHandler : IRequestHandler<RestoreIssueSlipFromBinCommand, IssueSlip>
    {
        public RestoreIssueSlipFromBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IssueSlip> Handle(RestoreIssueSlipFromBinCommand request, CancellationToken cancellationToken)
        {
            IssueSlip issueSlip = this.DatabaseContext.IssueSlips.FirstOrDefault(x => x.Id == request.IssueSlipId);

            if (issueSlip == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.IssueSlip_EntityNotFoundException, request.IssueSlipId));
            }
            if (issueSlip.CanBeRestoredFromBin() == false)
            {
                throw new EntityRestoreFromBinException(string.Format(Properties.Resources.IssueSlip_EntityRestoreFromBinException, request.IssueSlipId));
            }

            issueSlip.UtcMovedToBin = null;
            issueSlip.MovedToBinInCascade = false;

            foreach (IssueSlip.Item item in issueSlip.Items.Where(x => x.MovedToBinInCascade))
            {
                await this.Mediator.Send(new RestoreIssueSlipItemFromBinCommand(item.IssueSlipId, item.PositionId, item.WareId), cancellationToken);
            }

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return issueSlip;
        }
    }
}
