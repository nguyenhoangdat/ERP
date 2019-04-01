using MediatR;
using Restmium.ERP.Integration.Warehouse;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Domain
{
    public class WareDeletedDomainEventHandler : INotificationHandler<WareDeletedDomainEvent>
    {
        public WareDeletedDomainEventHandler(IEventBus eventBus)
        {
            this.EventBus = eventBus;
        }

        protected IEventBus EventBus { get; }

        public async Task Handle(WareDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            this.EventBus.Publish(new WareDeletedIntegrationEvent(notification.Ware.ProductId));
        }
    }
}
