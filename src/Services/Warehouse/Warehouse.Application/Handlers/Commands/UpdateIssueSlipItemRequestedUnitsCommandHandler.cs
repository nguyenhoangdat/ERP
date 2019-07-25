using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateIssueSlipItemRequestedUnitsCommandHandler : IRequestHandler<UpdateIssueSlipItemRequestedUnitsCommand, IssueSlip.Item>
    {
        public UpdateIssueSlipItemRequestedUnitsCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IssueSlip.Item> Handle(UpdateIssueSlipItemRequestedUnitsCommand request, CancellationToken cancellationToken)
        {
            IssueSlip.Item item = await this.DatabaseContext.IssueSlipItems.FindAsync(new object[] { request.IssueSlipId, request.WareId }, cancellationToken);
            item.RequestedUnits = request.RequestedUnits;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new IssueSlipItemUpdatedDomainEvent(item), cancellationToken);

            return item;
        }
    }
}
