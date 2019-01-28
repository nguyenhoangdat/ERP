using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class DeleteIssueSlipCommandHandler : IRequestHandler<DeleteIssueSlipCommand, IssueSlip>
    {
        public DeleteIssueSlipCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IssueSlip> Handle(DeleteIssueSlipCommand request, CancellationToken cancellationToken)
        {
            // Find IssueSlip and throw an Exception if it does not exist
            IssueSlip issueSlip = await this.DatabaseContext.IssueSlips.FindAsync(new object[] { request.Model.Id }, cancellationToken);
            if (issueSlip == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["IssueSlip_Delete_EntityNotFoundException"], request.Model.Id));
            }

            // Delete and Save
            issueSlip = this.DatabaseContext.IssueSlips.Remove(issueSlip).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            // Publish DomainEvent that the IssueSlip has been created
            await this.Mediator.Publish(new IssueSlipDeletedDomainEvent(issueSlip), cancellationToken);

            return issueSlip;
        }
    }
}
