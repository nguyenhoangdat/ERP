using MediatR;
using Entities = Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindWarehouseByPositionIdCommand : IRequest<Entities.Warehouse>
    {
        public FindWarehouseByPositionIdCommand(long positionId)
        {
            this.PositionId = positionId;
        }

        public long PositionId { get; }
    }
}
