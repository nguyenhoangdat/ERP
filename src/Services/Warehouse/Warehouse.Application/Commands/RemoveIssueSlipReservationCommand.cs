using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RemoveIssueSlipReservationCommand : IRequest<Position>
    {
        public RemoveIssueSlipReservationCommand(Position position, int reservedUnitsToRemove)
        {
            this.Position = position;
            this.ReservedUnitsToRemove = reservedUnitsToRemove;
        }

        public Position Position { get; }
        public int ReservedUnitsToRemove { get; }
    }
}
