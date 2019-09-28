using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindWaresInWarehouseCommand : IRequest<IEnumerable<Ware>>
    {
        public FindWaresInWarehouseCommand(int warehouseId)
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
