using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RemoveIssueSlipReservationCommand : IRequest<Position>
    {
        public RemoveIssueSlipReservationCommand(long positionId, int reservedUnitsToRemove)
        {
            if (positionId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(positionId));
            }
            this.PositionId = positionId;
            this.ReservedUnitsToRemove = reservedUnitsToRemove;
        }

        public long PositionId { get; }
        public int ReservedUnitsToRemove { get; }
    }
}
