using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities = Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Domain
{
    public class PositionRelocatedDomainEventHandler : INotificationHandler<PositionRelocatedDomainEvent>
    {
        public PositionRelocatedDomainEventHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task Handle(PositionRelocatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // Update ReservedUnits
            Position positionFrom = await this.Mediator.Send(new RemoveIssueSlipReservationCommand(notification.PositionFrom, notification.RelocatedUnits), cancellationToken);
            Position positionTo = await this.Mediator.Send(new CreateIssueSlipReservationCommand(notification.PositionTo.Id, notification.RelocatedUnits), cancellationToken);

            if (!positionFrom.HasAllIssueSlipItemsProcessed())
            {
                await this.Mediator.Send(new SplitAndMoveUnprocessedIssueSlipItemsBetweenPositionsCommand(positionFrom.Id, positionTo.Id), cancellationToken);
            }
            if (!positionFrom.HasAllReceiptItemsProcessed())
            {
                await this.Mediator.Send(new SplitAndMoveUnprocessedReceiptItemsBetweenPositionsCommand(positionFrom.Id, positionTo.Id), cancellationToken);
            }
            if (!positionFrom.HasAllStockTakingItemsProcessed())
            {
                await this.Mediator.Send(new MoveUnprocessedStockTakingItemsBetweenPositionsCommand(positionFrom.Id, positionTo.Id), cancellationToken);
            }
        }
    }
}
