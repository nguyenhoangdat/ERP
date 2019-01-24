using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateMovementCommand : IRequest<Movement>
    {
        public UpdateMovementCommand(UpdateMovementCommandModel model)
        {
            this.Model = model;
        }

        public UpdateMovementCommandModel Model { get; }

        public class UpdateMovementCommandModel
        {
            public UpdateMovementCommandModel(long id, int wareId, long positionId, Movement.Direction direction, int countChange)
            {
                this.Id = id;
                this.WareId = wareId;
                this.PositionId = positionId;
                this.Direction = direction;
                this.CountChange = countChange;
            }

            public long Id { get; }
            public int WareId { get; }
            public long PositionId { get; }
            public Movement.Direction Direction { get; }
            public int CountChange { get; }
        }
    }
}
