using MediatR;
using Restmium.ERP.Integration.Warehouse;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.Messaging;
using System.Collections.Generic;
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

        public async Task Handle(IssueSlipCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            IssueSlip issueSlip = notification.IssueSlip;

            this.EventBus.Publish(new IssueSlipCreatedIntegrationEvent(issueSlip.OrderId, issueSlip.UtcDispatchDate, issueSlip.UtcDeliveryDate, this.GetItems(notification.IssueSlip.Items)));
        }

        public IEnumerable<IssueSlipCreatedIntegrationEvent.IssueSlipItem> GetItems(IEnumerable<IssueSlip.Item> items)
        {
            List<IssueSlipCreatedIntegrationEvent.IssueSlipItem> output = new List<IssueSlipCreatedIntegrationEvent.IssueSlipItem>();

            foreach (IssueSlip.Item item in items)
            {
                output.Add(new IssueSlipCreatedIntegrationEvent.IssueSlipItem(item.Ware.ProductId, item.RequestedUnits, item.Ware.Width, item.Ware.Height, item.Ware.Depth, item.Ware.Weight));
            }

            return output;
        }
    }
}
