using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MovePositionToBinCommand : IRequest<Position>
    {
        public MovePositionToBinCommand(long positionId, bool movedToBinInCascade)
        {
            this.PositionId = positionId;
            this.MovedToBinInCascade = movedToBinInCascade;
        }

        public long PositionId { get; }
        public bool MovedToBinInCascade { get; }
    }
}
