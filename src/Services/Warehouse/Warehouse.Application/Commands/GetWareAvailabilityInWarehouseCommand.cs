using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class GetWareAvailabilityInWarehouseCommand : IRequest<WareAvailabilityInWarehouse>
    {
        public GetWareAvailabilityInWarehouseCommand(int wareId, int warehouseId)
        {
            this.WareId = wareId;
            this.WarehouseId = warehouseId;
        }

        public int WareId { get; }
        public int WarehouseId { get; }
    }
}
