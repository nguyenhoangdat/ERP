using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Domain
{
    public class PositionRelocatedDomainEventHandler : INotificationHandler<PositionRelocatedDomainEvent>
    {
        public PositionRelocatedDomainEventHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task Handle(PositionRelocatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // Find all IssueSlip.Items which were not processed and are related to Position from which was the relocation done
            foreach (Warehouse.Domain.Entities.IssueSlip.Item item in this.DatabaseContext.IssueSlipItems.Where(x => x.PositionId == notification.PositionFrom.Id && x.IssuedUnits == 0))
            {
                await this.Mediator.Send(new UpdateIssueSlipItemCommand(item.WareId, item.IssueSlipId, notification.PositionTo.Id, item.IssuedUnits));
            }

            //TODO: Should I publish DomainEvent? - One is already published by the UpdateIssueSlipItemCommandHandler
        }
    }
}
