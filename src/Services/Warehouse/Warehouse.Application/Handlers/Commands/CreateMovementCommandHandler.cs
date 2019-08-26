using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateMovementCommandHandler : IRequestHandler<CreateMovementCommand, Movement>
    {
        public CreateMovementCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Movement> Handle(CreateMovementCommand request, CancellationToken cancellationToken)
        {
            // Check possible conflict and throw exception if conflict is found
            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == request.PositionId);
            Ware ware = position.GetWare();

            // Throw an exception if position holds a different ware
            if (ware != null && ware.Id != request.WareId)
            {
                throw new PositionWareConflictException(string.Format(Properties.Resources.Movement_Create_PositionWareConflictException,
                    (request.Direction == Movement.Direction.In) ? "store" : "retrieve",
                    request.WareId,
                    request.PositionId,
                    ware.Id));
            }

            // Throw an exception if position doesn't hold enough units
            if (request.Direction == Movement.Direction.Out && position.CountWare() < request.CountChange)
            {
                throw new PositionEmptyException(string.Format(Properties.Resources.Movement_Create_PositionEmptyException, request.WareId, request.PositionId));
            }

            int newCount = position.CountWare() + request.CountChange;

            if (request.Direction == Movement.Direction.In)
            {
                // Check capacity (space)
                if (!position.HasSpaceCapacity(ware, newCount))
                {
                    throw new PositionSpaceCapacityException(string.Format(Properties.Resources.Movement_Create_PositionSpaceCapacityException, ware.Id, position.Id, request.CountChange));
                }
                // Check load capacity (weight)
                if (!position.HasLoadCapacity(ware, newCount))
                {
                    throw new PositionLoadCapacityException(string.Format(Properties.Resources.Movement_Create_PositionLoadCapacityException, ware.Id, position.Id, request.CountChange));
                }
            }

            // Create Movement
            Movement movement = this.DatabaseContext.Movements.Add(new Movement(request.WareId, request.PositionId, request.Direction, request.CountChange, newCount)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            // Publish Notification (Domain Event)
            await this.Mediator.Publish(new MovementCreatedDomainEvent(movement), cancellationToken);

            return movement;
        }
    }
}
