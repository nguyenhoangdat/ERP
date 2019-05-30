using MediatR;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveWarehouseToBinCommand : IRequest<Domain.Entities.Warehouse>
    {
        public MoveWarehouseToBinCommand(int warehouseId)
        {
            this.WarehouseId = warehouseId;
        }

        public int WarehouseId { get; }
    }
}
