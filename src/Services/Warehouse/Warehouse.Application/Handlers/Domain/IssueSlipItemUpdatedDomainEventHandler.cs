using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Domain
{
    public class IssueSlipItemUpdatedDomainEventHandler : INotificationHandler<IssueSlipItemUpdatedDomainEvent>
    {
        public async Task Handle(IssueSlipItemUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            //TODO: Notify all clients through SignalR that the IssueSlip.Item has been updated

            throw new NotImplementedException();
        }
    }
}
