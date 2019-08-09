using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RemoveIssueSlipReservationCommand : IRequest<Position>
    {
        public RemoveIssueSlipReservationCommand(long positionId, int reservedUnitsToRemove)
        {
            this.PositionId = positionId;
            this.ReservedUnitsToRemove = reservedUnitsToRemove;
        }

        public long PositionId { get; }
        public int ReservedUnitsToRemove { get; }
    }
}
