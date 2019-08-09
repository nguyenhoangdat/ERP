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
            IssueSlip issueSlip = await this.DatabaseContext.IssueSlips.FindAsync(new object[] { request.IssueSlipId }, cancellationToken);

            if (issueSlip == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["IssueSlip_EntityNotFoundException"], request.IssueSlipId));
            }

            issueSlip.UtcMovedToBin = DateTime.UtcNow;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            foreach (IssueSlip.Item item in issueSlip.Items)
            {
                await this.Mediator.Send(new MoveIssueSlipItemToBinCommand(item.IssueSlipId, item.PositionId.Value, item.WareId), cancellationToken);
            }

            return issueSlip;
        }
    }
}
