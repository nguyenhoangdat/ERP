using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RelocatePositionCommand : IRequest<Position>
    {
        public RelocatePositionCommand(long fromPositionWithId, long toPositionWithId)
        {
            if (fromPositionWithId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(fromPositionWithId));
            }
            this.FromPositionWithId = fromPositionWithId;

            if (toPositionWithId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(toPositionWithId));
            }
            this.ToPositionWithId = toPositionWithId;
        }

        public long FromPositionWithId { get; }
        public long ToPositionWithId { get; }
    }
}
