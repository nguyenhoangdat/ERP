using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Domain
{
    public class MovementCreatedDomainEventHandler : INotificationHandler<MovementCreatedDomainEvent>
    {
        public MovementCreatedDomainEventHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task Handle(MovementCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            StockTaking.Item item = this.DatabaseContext.StockTakingItems.FirstOrDefault(x =>
                x.PositionId == notification.PositionId &&
                x.WareId == notification.WareId &&
                x.UtcCounted == null);

            if (item != null)
            {
                item.CurrentStock = notification.CountTotal;
                await this.DatabaseContext.SaveChangesAsync(cancellationToken);

                await this.Mediator.Publish(new StockTakingItemUpdatedDomainEvent(item), cancellationToken);
            }
        }
    }
}
