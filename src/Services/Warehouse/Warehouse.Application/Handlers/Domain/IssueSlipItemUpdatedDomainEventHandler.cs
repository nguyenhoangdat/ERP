using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Domain
{
    public class IssueSlipItemUpdatedDomainEventHandler : INotificationHandler<IssueSlipItemUpdatedDomainEvent>
    {
        public IssueSlipItemUpdatedDomainEventHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task Handle(IssueSlipItemUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            IssueSlip issueSlip = notification.Item.IssueSlip;

            bool allProcessed = true;
            foreach (IssueSlip.Item item in issueSlip.Items)
            {
                if (item.IssuedUnits < item.RequestedUnits)
                {
                    allProcessed = false;
                    break;
                }
            }

            if (allProcessed)
            {
                issueSlip.UtcProcessed = DateTime.UtcNow;
                await this.DatabaseContext.SaveChangesAsync(cancellationToken);

                await this.Mediator.Publish(new IssueSlipProcessedDomainEvent(issueSlip), cancellationToken);
            }
        }
    }
}
