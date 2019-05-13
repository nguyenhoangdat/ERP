using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateMovementCommand : IRequest<Movement>
    {
        public CreateMovementCommand(int wareId, long positionId, Movement.Direction direction, int countChange)
        {
            this.WareId = wareId;
            this.PositionId = positionId;
            this.Direction = direction;
            this.CountChange = countChange;
        }

        public int WareId { get; }
        public long PositionId { get; }
        public Movement.Direction Direction { get; }
        public int CountChange { get; }
    }
}
