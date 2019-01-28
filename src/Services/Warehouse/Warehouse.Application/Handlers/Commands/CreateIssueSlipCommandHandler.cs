using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
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
            // Find positions for IssueSlip.Items and create them
            List<IssueSlip.Item> items = new List<IssueSlip.Item>(request.Model.Items.Count);
            foreach (CreateIssueSlipCommand.CreateIssueSlipCommandModel.Item item in request.Model.Items)
            {
                foreach (KeyValuePair<Position, int> valuePair in await this.FindPositions(item.Ware, item.RequstedUnits, cancellationToken))
                {
                    items.Add(new IssueSlip.Item(0, item.Ware.Id, valuePair.Key.Id, valuePair.Value, 0, 0));
                }
            }

            // Create IssueSlip and save it to database
            IssueSlip issueSlip = this.DatabaseContext.IssueSlips.Add(new IssueSlip(request.Model.Name, request.Model.OrderId, request.Model.UtcDispatchDate, request.Model.UtcDeliveryDate, items)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            // Publish DomainEvent that the IssueSlip has been created
            await this.Mediator.Publish(new IssueSlipCreatedDomainEvent(issueSlip), cancellationToken);

            return issueSlip;
        }

        protected async Task<Dictionary<Position, int>> FindPositions(Ware ware, int units, CancellationToken cancellationToken)
        {
            Dictionary<Position, int> pairs = new Dictionary<Position, int>();

            // Get all positions where Ware is stored
            IEnumerable<Position> positions = this.DatabaseContext.Positions
                .Where(x => x.GetWare() == ware)
                .OrderBy(x => x.Rating)
                .ThenByDescending(x => x.CountWare())
                .ToList();

            // Issue units from positions + create reservations
            foreach (Position position in positions)
            {
                int unitsAtPosition = position.CountWare();

                if (unitsAtPosition < units)
                {
                    await this.Mediator.Send(new CreateIssueSlipReservationCommand(position.Id, unitsAtPosition), cancellationToken);
                    pairs.Add(position, unitsAtPosition);
                    units -= unitsAtPosition;
                }
                else
                {
                    await this.Mediator.Send(new CreateIssueSlipReservationCommand(position.Id, units), cancellationToken);
                    pairs.Add(position, units);
                    break;
                }
            }

            return pairs;
        }
    }
}
