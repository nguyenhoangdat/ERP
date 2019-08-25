using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class IssueUnitsForIssueSlipItemCommandHandler : IRequestHandler<IssueUnitsForIssueSlipItemCommand, IssueSlip.Item>
    {
        public IssueUnitsForIssueSlipItemCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IssueSlip.Item> Handle(IssueUnitsForIssueSlipItemCommand request, CancellationToken cancellationToken)
        {
             IQueryable<IssueSlip.Item> items = this.DatabaseContext.IssueSlipItems
                .Where(x => x.IssueSlipId == request.IssueSlipId && x.WareId == request.WareId);

            IssueSlip.Item item = items.FirstOrDefault(x => x.PositionId == request.PositionId);
            IssueSlip.Item unassignedItem = items.FirstOrDefault(x => x.PositionId == null);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["IssueSlipItem_EntityNotFoundException"], request.IssueSlipId, request.PositionId, request.WareId));
            }

            int unitsRemainingToIssue = item.RequestedUnits - item.IssuedUnits;
            if (unitsRemainingToIssue < request.Count) // Trying to issue more units than requested
            {
                int extraUnits = request.Count - unitsRemainingToIssue; // 1 = 10 - 9 ==> trying to issue 1 extra unit

                if (extraUnits > item.Position.CountAvailableWare())
                {
                    // Position doesn't hold enough units to successfully issue them
                    throw new IssueSlipItemPositionAvailableUnitsException(string.Format(Resources.Exceptions.Values["IssueSlipItem_PositionAvailableUnitsException"], request.IssueSlipId, request.PositionId, request.WareId));
                }
                else if (unassignedItem != null && extraUnits <= unassignedItem.RequestedUnits)
                {
                    // Transfer requested units (extra units)
                    await this.Mediator.Send(new UpdateIssueSlipItemRequestedUnitsCommand(item.IssueSlipId, item.PositionId, item.WareId, item.RequestedUnits + extraUnits), cancellationToken);
                    await this.Mediator.Send(new UpdateIssueSlipItemRequestedUnitsCommand(unassignedItem.IssueSlipId, unassignedItem.PositionId, unassignedItem.WareId, unassignedItem.RequestedUnits - extraUnits), cancellationToken);

                    // Issue additional units
                    item.IssuedUnits += request.Count;
                    await this.DatabaseContext.SaveChangesAsync(cancellationToken);

                    // Remove reservation
                    await this.Mediator.Send(new RemoveIssueSlipReservationCommand(item.PositionId.Value, request.Count), cancellationToken);

                    // Create movement
                    await this.Mediator.Send(new CreateMovementCommand(item.WareId, item.PositionId.Value, Movement.Direction.Out, request.Count), cancellationToken);
                }
                else
                {
                    // We don't need to issue more units. => Throw an exception
                    throw new UnitsExceededException(string.Format(Resources.Exceptions.Values["IssueSlipItem_RequestedUnitsExceededException"], request.IssueSlipId, request.PositionId, request.WareId));
                }
            }
            else
            {
                item.IssuedUnits += request.Count;
                await this.DatabaseContext.SaveChangesAsync(cancellationToken);

                await this.Mediator.Send(new RemoveIssueSlipReservationCommand(item.PositionId.Value, request.Count), cancellationToken);

                // Transfer unissued units (units remaining to issue)
                if (item.IssuedUnits < item.RequestedUnits)
                {
                    unitsRemainingToIssue = item.RequestedUnits - item.IssuedUnits;
                    
                    if (unassignedItem == null)
                    {
                        await this.Mediator.Send(new CreateIssueSlipItemCommand(item.IssueSlipId, item.WareId, null, unitsRemainingToIssue, 0), cancellationToken);
                    }
                    else
                    {
                        await this.Mediator.Send(new UpdateIssueSlipItemRequestedUnitsCommand(unassignedItem.IssueSlipId, unassignedItem.PositionId, unassignedItem.WareId, unassignedItem.RequestedUnits + unitsRemainingToIssue), cancellationToken);
                    }

                    await this.Mediator.Send(new UpdateIssueSlipItemRequestedUnitsCommand(item.IssueSlipId, item.PositionId, item.WareId, item.IssuedUnits), cancellationToken);
                }

                await this.Mediator.Send(new CreateMovementCommand(item.WareId, item.PositionId.Value, Movement.Direction.Out, request.Count), cancellationToken);
            }

            await this.Mediator.Publish(new IssueSlipItemUpdatedDomainEvent(item), cancellationToken);

            return item;
        }
    }
}
