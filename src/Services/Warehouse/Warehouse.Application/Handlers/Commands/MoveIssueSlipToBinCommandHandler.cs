using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class MoveIssueSlipToBinCommandHandler : IRequestHandler<MoveIssueSlipToBinCommand, IssueSlip>
    {
        public MoveIssueSlipToBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IssueSlip> Handle(MoveIssueSlipToBinCommand request, CancellationToken cancellationToken)
        {
            IssueSlip issueSlip = this.DatabaseContext.IssueSlips.FirstOrDefault(x => x.Id == request.IssueSlipId);

            if (issueSlip == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.IssueSlip_EntityNotFoundException, request.IssueSlipId));
            }
            if (request.MovedToBinInCascade == false)
            {
                if (issueSlip.CanBeMovedToBin() == false)
                {
                    throw new EntityMoveToBinException(string.Format(Properties.Resources.IssueSlip_EntityMoveToBinException, request.IssueSlipId));
                }
            }

            issueSlip.UtcMovedToBin = DateTime.UtcNow;
            issueSlip.MovedToBinInCascade = request.MovedToBinInCascade;

            foreach (IssueSlip.Item item in issueSlip.Items.Where(x => x.UtcMovedToBin == null))
            {
                await this.Mediator.Send(new MoveIssueSlipItemToBinCommand(item.IssueSlipId, item.PositionId, item.WareId, true), cancellationToken);
            }

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return issueSlip;
        }
    }
}
