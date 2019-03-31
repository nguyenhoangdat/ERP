using MediatR;
using Restmium.ERP.Integration.Warehouse;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Domain
{
    public class IssueSlipCreatedDomainEventHandler : INotificationHandler<IssueSlipCreatedDomainEvent>
    {
        public IssueSlipCreatedDomainEventHandler(IEventBus eventBus)
        {
            this.EventBus = eventBus;
        }

        protected IEventBus EventBus { get; }

        public Task Handle(IssueSlipCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            IssueSlip issueSlip = notification.IssueSlip;

            this.EventBus.Publish(new IssueSlipCreatedIntegrationEvent(issueSlip.OrderId, issueSlip.UtcDispatchDate, issueSlip.UtcDeliveryDate));

            return Task.CompletedTask;
        }
    }
}
