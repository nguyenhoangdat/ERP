using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RestoreMovementFromBinCommand : IRequest<Movement>
    {
        public RestoreMovementFromBinCommand(long movementId)
        {
            this.MovementId = movementId;
        }

        public long MovementId { get; }
    }
}
