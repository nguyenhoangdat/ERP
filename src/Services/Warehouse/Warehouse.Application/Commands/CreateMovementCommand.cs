using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateMovementCommand : IRequest<Movement>
    {
        public CreateMovementCommand(CreateMovementCommandModel model)
        {
            this.Model = model;
        }
        public CreateMovementCommand(int wareId, long positionId, Movement.Direction direction, int countChange, int employeeId) : this(new CreateMovementCommandModel(wareId, positionId, direction, countChange, employeeId))
        {

        }

        public CreateMovementCommandModel Model { get; }

        public class CreateMovementCommandModel
        {
            public CreateMovementCommandModel(int wareId, long positionId, Movement.Direction direction, int countChange, int employeeId)
            {
                this.WareId = wareId;
                this.PositionId = positionId;
                this.Direction = direction;
                this.CountChange = countChange;
                this.EmployeeId = employeeId;
            }

            public int WareId { get; }
            public long PositionId { get; } 
            public Movement.Direction Direction { get; }
            public int CountChange { get; }
            public int EmployeeId { get; }
        }
    }
}
