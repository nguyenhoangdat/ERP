using MediatR;
using Restmium.ERP.Integration.Warehouse;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Domain
{
    public class WareCreatedDomainEventHandler : INotificationHandler<WareCreatedDomainEvent>
    {
        public WareCreatedDomainEventHandler(IEventBus eventBus)
        {
            this.EventBus = eventBus;
        }

        protected IEventBus EventBus { get; }

        public async Task Handle(WareCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            this.EventBus.Publish(new WareCreatedIntegrationEvent(notification.Ware.ProductId));
        }
    }
}
