using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RestorePositionFromBinCommand : IRequest<Position>
    {
        public RestorePositionFromBinCommand(long positionId)
        {
            this.PositionId = positionId;
        }

        public long PositionId { get; }
    }
}
