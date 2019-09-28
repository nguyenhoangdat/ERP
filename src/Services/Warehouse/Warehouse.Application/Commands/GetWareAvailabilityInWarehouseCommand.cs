using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class GetWareAvailabilityInWarehouseCommand : IRequest<WareAvailabilityInWarehouse>
    {
        public GetWareAvailabilityInWarehouseCommand(int wareId, int warehouseId)
        {
            this.WareId = wareId;

            if (warehouseId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(warehouseId));
            }
            this.WarehouseId = warehouseId;
        }

        public int WareId { get; }
        public int WarehouseId { get; }
    }
}
