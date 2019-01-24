using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Domain
{
    public class IssueSlipReservationRemovedDomainEventHandler : INotificationHandler<IssueSlipReservationRemovedDomainEvent>
    {
        public async Task Handle(IssueSlipReservationRemovedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
