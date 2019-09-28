using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class GetWarehouseCurrentCapacityCommand : IRequest<WarehouseCapacity>
    {
        public GetWarehouseCurrentCapacityCommand(int warehouseId)
        {
            if (warehouseId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(warehouseId));
            }

            this.WarehouseId = warehouseId;
        }

        public int WarehouseId { get; }
    }
}
