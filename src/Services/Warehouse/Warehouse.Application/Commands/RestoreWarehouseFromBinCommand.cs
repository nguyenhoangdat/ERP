using MediatR;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RestoreWarehouseFromBinCommand : IRequest<Domain.Entities.Warehouse>
    {
        public RestoreWarehouseFromBinCommand(int warehouseId)
        {
            this.WarehouseId = warehouseId;
        }

        public int WarehouseId { get; }
    }
}
