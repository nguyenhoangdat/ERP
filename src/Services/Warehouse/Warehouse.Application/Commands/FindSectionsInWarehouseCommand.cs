using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindSectionsInWarehouseCommand : IRequest<IEnumerable<Section>>
    {
        public FindSectionsInWarehouseCommand(int warehouseId)
        {
            this.WarehouseId = warehouseId;
        }

        public int WarehouseId { get; }
    }
}
