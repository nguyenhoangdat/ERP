using MediatR;
using Restmium.ERP.Integration.Warehouse;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Domain
{
    public class WareRenamedDomainEventHandler : INotificationHandler<WareRenamedDomainEvent>
    {
        public WareRenamedDomainEventHandler(IEventBus eventBus)
        {
            this.EventBus = eventBus;
        }

        protected IEventBus EventBus { get; }

        public async Task Handle(WareRenamedDomainEvent notification, CancellationToken cancellationToken)
        {
            this.EventBus.Publish(new WareRenamedIntegrationEvent(notification.Ware.ProductId));
        }
    }
}
