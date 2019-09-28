using MediatR;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RestoreWarehouseFromBinCommand : IRequest<Domain.Entities.Warehouse>
    {
        public RestoreWarehouseFromBinCommand(int warehouseId)
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
