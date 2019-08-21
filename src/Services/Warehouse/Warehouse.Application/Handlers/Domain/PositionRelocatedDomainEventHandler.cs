using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
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
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task Handle(PositionRelocatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Position positionFrom = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == notification.PositionFrom.Id);
            Position positionTo = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == notification.PositionTo.Id);

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
