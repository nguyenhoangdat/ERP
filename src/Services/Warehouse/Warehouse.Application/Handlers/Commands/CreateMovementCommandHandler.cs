using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
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
        protected const string CreateMovementCommandHandlerPositionConflictException = "Unable to create Movement to {0} Ware with Id={1} at Position with Id={2}. Position contains another Ware with Id={3}!";
        protected const string CreateMovementCommandHandlerEmptyPositionException = "Unable to create Movement to retrieve Ware with Id={1} at Position with Id={2}. Position doesn't hold enough units!";

        public CreateMovementCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Movement> Handle(CreateMovementCommand request, CancellationToken cancellationToken)
        {
            // Check possible conflict and throw exception if conflict is found
            Position position = this.DatabaseContext.Positions.Find(request.Model.PositionId);
            Ware wareAtPosition = this.DatabaseContext.Positions.GetWare(position);
            int countOfWareAtPosition = this.DatabaseContext.Positions.Count(position);

            if (wareAtPosition != null && wareAtPosition.Id != request.Model.WareId && countOfWareAtPosition != 0)
            {
                throw new PositionConflictException(string.Format(CreateMovementCommandHandlerPositionConflictException,
                    (request.Model.Direction == Movement.Direction.In) ? "store" : "retrieve",
                    request.Model.WareId,
                    request.Model.PositionId,
                    wareAtPosition.Id));
            }

            // Throw an exception if position doesn't hold enough units
            if (request.Model.Direction == Movement.Direction.Out && countOfWareAtPosition < request.Model.CountChange)
            {
                throw new EmptyPositionException(string.Format(CreateMovementCommandHandlerEmptyPositionException, request.Model.WareId, request.Model.PositionId));
            }

            Movement lastMovement = this.DatabaseContext.Movements.Where(x => x.PositionId == request.Model.PositionId).OrderByDescending(x => x.UtcCreated).FirstOrDefault();
            int countTotal = lastMovement?.CountTotal ?? 0;

            Ware ware = this.DatabaseContext.Wares.Find(request.Model.WareId);
            // Check capacity (space)
            if (!this.HasSpaceCapacity(position, ware, countTotal))
            {
                throw new NotImplementedException();
            }
            // Check load capacity (weight)
            if (!this.HasLoadCapacity(position, ware, countTotal))
            {
                throw new NotImplementedException();
            }

            // Create Movement
            Movement movement = new Movement(request.Model.WareId, request.Model.PositionId, request.Model.Direction, request.Model.Content, request.Model.CountChange, countTotal + request.Model.CountChange);
            this.DatabaseContext.Movements.Add(movement);
            await this.DatabaseContext.SaveChangesAsync();

            return movement;
        }

        protected bool HasSpaceCapacity(Position position, Ware ware, int countTotal)
        {
            return false; //TODO: Implement (Challenge task)
        }
        protected bool HasLoadCapacity(Position position, Ware ware, int countTotal)
        {
            return position.MaxWeight < ware.Weight * countTotal;
        }
    }
}
