using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class MoveMovementToBinCommandHandler : IRequestHandler<MoveMovementToBinCommand, Movement>
    {
        public MoveMovementToBinCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Movement> Handle(MoveMovementToBinCommand request, CancellationToken cancellationToken)
        {
            Movement movement = this.DatabaseContext.Movements.FirstOrDefault(x => x.Id == request.MovementId);

            if (movement == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Movement_EntityNotFoundException, request.MovementId));
            }
            if (request.MovedToBinInCascade == false)
            {
                if (movement.CanBeMovedToBin() == false)
                {
                    throw new EntityMoveToBinException(string.Format(Properties.Resources.Movement_EntityMoveToBinException, request.MovementId));
                }
            }            

            movement.UtcMovedToBin = DateTime.UtcNow;
            movement.MovedToBinInCascade = request.MovedToBinInCascade;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return movement;
        }
    }
}
