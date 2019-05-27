using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateIssueSlipCommandHandler : IRequestHandler<UpdateIssueSlipCommand, IssueSlip>
    {
        public UpdateIssueSlipCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IssueSlip> Handle(UpdateIssueSlipCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.IssueSlips.Any(x => x.Id == request.Id))
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["IssueSlip_Update_EntityNotFoundException"], request.Id));
            }

            IssueSlip issueSlip = await this.DatabaseContext.IssueSlips.FindAsync(new object[] { request.Id }, cancellationToken);
            issueSlip.UtcDeliveryDate = request.UtcDeliveryDate;
            issueSlip.UtcDispatchDate = request.UtcDispatchDate;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new IssueSlipUpdatedDomainEvent(issueSlip), cancellationToken);

            return issueSlip;
        }
    }
}
