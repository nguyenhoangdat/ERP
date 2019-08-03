using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateIssueSlipItemCommandHandler : IRequestHandler<CreateIssueSlipItemCommand, IssueSlip.Item>
    {
        public CreateIssueSlipItemCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IssueSlip.Item> Handle(CreateIssueSlipItemCommand request, CancellationToken cancellationToken)
        {
            IssueSlip.Item item = new IssueSlip.Item(request.IssueSlipId, request.WareId, request.PositionId, request.RequestedUnits, request.IssuedUnits);

            item = (await this.DatabaseContext.IssueSlipItems.AddAsync(item, cancellationToken)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new IssueSlipItemCreatedDomainEvent(item), cancellationToken);
            await this.Mediator.Send(new CreateIssueSlipReservationCommand(item.PositionId.Value, item.RequestedUnits), cancellationToken);

            return item;
        }
    }
}
