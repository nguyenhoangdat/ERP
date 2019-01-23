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
            StockTaking.Item item = this.DatabaseContext.StockTakingItems.Where(x =>
                x.PositionId == notification.Movement.PositionId &&
                x.WareId == notification.Movement.WareId &&
                x.UtcCounted == null).FirstOrDefault();

            if (item != null)
            {
                item.CurrentStock = notification.Movement.CountTotal;

                item = this.DatabaseContext.StockTakingItems.Update(item).Entity;
                await this.DatabaseContext.SaveChangesAsync(cancellationToken);

                await this.Mediator.Publish(new StockTakingItemUpdatedDomainEvent(item));
            }
        }
    }
}
