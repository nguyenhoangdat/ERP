using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateMovementCommandHandler : IRequestHandler<CreateMovementCommand, Movement>
    {
        //TODO: Check grammar
        protected const string CreateMovementCommandHandlerPositionWareConflictException = "Unable to create Movement to {0} Ware with Id={1} at Position with Id={2}. Position contains another Ware with Id={3}!";
        protected const string CreateMovementCommandHandlerPositionEmptyException = "Unable to create Movement to retrieve Ware with Id={1} at Position with Id={2}. Position doesn't hold enough units!";
        protected const string CreateMovementCommandHandlerPositionLoadCapacityException = "Unable to store Ware ({0}) at Position ({1}) in amount of {2} units. Load capacity exceeded.";

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
            Position position = this.DatabaseContext.Positions.Find(request.Model.PositionId);
            Ware wareAtPosition = this.DatabaseContext.Positions.GetWare(position);
            int currentCount = this.DatabaseContext.Positions.Count(position);
            int newCount = currentCount + request.Model.CountChange;

            if (wareAtPosition != null && wareAtPosition.Id != request.Model.WareId && currentCount != 0)
            {
                throw new PositionWareConflictException(string.Format(CreateMovementCommandHandlerPositionWareConflictException,
                    (request.Model.Direction == Movement.Direction.In) ? "store" : "retrieve",
                    request.Model.WareId,
                    request.Model.PositionId,
                    wareAtPosition.Id));
            }

            // Throw an exception if position doesn't hold enough units
            if (request.Model.Direction == Movement.Direction.Out && currentCount < request.Model.CountChange)
            {
                throw new PositionEmptyException(string.Format(CreateMovementCommandHandlerPositionEmptyException, request.Model.WareId, request.Model.PositionId));
            }

            Ware ware = this.DatabaseContext.Wares.Find(request.Model.WareId);
            // Check capacity (space)
            if (!this.HasSpaceCapacity(position, ware, newCount))
            {
                throw new NotImplementedException();
            }
            // Check load capacity (weight)
            if (!this.HasLoadCapacity(position, ware, newCount))
            {
                throw new PositionLoadCapacityException(string.Format(CreateMovementCommandHandlerPositionLoadCapacityException, ware.Id, position.Id, request.Model.CountChange));
            }

            // Create Movement
            Movement movement = this.DatabaseContext.Movements.Add(new Movement(request.Model.WareId, request.Model.PositionId, request.Model.Direction, request.Model.Content, request.Model.CountChange, countTotal + request.Model.CountChange, request.Model.EmployeeId)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            // Publish Notification (Domain Event)
            await this.Mediator.Publish(new MovementCreatedDomainEvent(movement));

            return movement;
        }

        protected bool HasSpaceCapacity(Position position, Ware ware, int countTotal)
        {
            return true; //TODO: Implement (Challenge task)
        }
        protected bool HasLoadCapacity(Position position, Ware ware, int countTotal)
        {
            return position.MaxWeight < ware.Weight * countTotal;
        }
    }
}
