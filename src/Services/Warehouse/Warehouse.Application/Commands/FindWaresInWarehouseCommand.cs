using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindWaresInWarehouseCommand : IRequest<IEnumerable<Ware>>
    {
        public FindWaresInWarehouseCommand(int warehouseId)
        {
            this.WarehouseId = warehouseId;
        }

        public int WarehouseId { get; }
    }
}
