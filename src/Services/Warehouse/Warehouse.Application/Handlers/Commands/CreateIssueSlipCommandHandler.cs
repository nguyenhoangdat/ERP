using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateIssueSlipCommandHandler : IRequestHandler<CreateIssueSlipCommand, IssueSlip>
    {
        public CreateIssueSlipCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IssueSlip> Handle(CreateIssueSlipCommand request, CancellationToken cancellationToken)
        {
            IssueSlip issueSlip = (await this.DatabaseContext.IssueSlips.AddAsync(this.ConvertToIssueSlip(request), cancellationToken)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.CreateItemsReservationsAsync(issueSlip.Items, cancellationToken);

            // Publish DomainEvent that the IssueSlip has been created
            await this.Mediator.Publish(new IssueSlipCreatedDomainEvent(issueSlip), cancellationToken);

            return issueSlip;
        }

        protected IssueSlip ConvertToIssueSlip(CreateIssueSlipCommand request)
        {
            // Convert IssueSlipCommand.Items to IssueSlip.Items
            List<IssueSlip.Item> items = new List<IssueSlip.Item>(request.Items.Count());
            foreach (CreateIssueSlipCommand.Item item in request.Items)
            {
                items.Add(new IssueSlip.Item(0, item.WareId, item.PositionId, item.RequstedUnits, 0));
            }

            return new IssueSlip(request.OrderId, request.UtcDispatchDate, request.UtcDeliveryDate, items);
        }

        public async Task CreateItemsReservationsAsync(IEnumerable<IssueSlip.Item> items, CancellationToken cancellationToken)
        {
            foreach (IssueSlip.Item item in items)
            {
                if (item.PositionId != null)
                {
                    await this.Mediator.Send(new CreateIssueSlipReservationCommand(item.PositionId.Value, item.RequestedUnits), cancellationToken);
                }
            }
        }
    }
}
