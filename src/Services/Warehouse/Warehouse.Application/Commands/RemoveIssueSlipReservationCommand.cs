using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RemoveIssueSlipReservationCommand : IRequest<Position>
    {
        public RemoveIssueSlipReservationCommand(RemoveReservationCommandModel model)
        {
            this.Model = model;
        }
        public RemoveIssueSlipReservationCommand(Position position, int reservedUnitsToRemove) : this(new RemoveReservationCommandModel(position, reservedUnitsToRemove))
        {
        }

        public RemoveReservationCommandModel Model { get; }

        public class RemoveReservationCommandModel
        {
            public RemoveReservationCommandModel(Position position, int reservedUnitsToRemove)
            {
                this.Position = position;
                this.ReservedUnitsToRemove = reservedUnitsToRemove;
            }

            public Position Position { get; }
            public int ReservedUnitsToRemove { get; }
        }
    }
}
