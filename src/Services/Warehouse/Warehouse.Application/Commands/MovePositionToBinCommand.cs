using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MovePositionToBinCommand : IRequest<Position>
    {
        public MovePositionToBinCommand(long positionId)
        {
            this.PositionId = positionId;
        }

        public long PositionId { get; }
    }
}
