using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveMovementToBinCommand : IRequest<Movement>
    {
        public MoveMovementToBinCommand(long movementId, bool movedToBinInCascade)
        {
            this.MovementId = movementId;
            this.MovedToBinInCascade = movedToBinInCascade;
        }

        public long MovementId { get; }
        public bool MovedToBinInCascade { get; }
    }
}
