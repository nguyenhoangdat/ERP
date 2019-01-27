using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindSectionsInWarehouseCommand : IRequest<IEnumerable<Section>>
    {
        public FindSectionsInWarehouseCommand(FindSectionInWarehouseCommandModel model)
        {
            this.Model = model;
        }
        public FindSectionsInWarehouseCommand(int warehouseId) : this(new FindSectionInWarehouseCommandModel(warehouseId)) { }

        public FindSectionInWarehouseCommandModel Model { get; }
        
        public class FindSectionInWarehouseCommandModel
        {
            public FindSectionInWarehouseCommandModel(int warehouseId)
            {
                this.WarehouseId = warehouseId;
            }

            public int WarehouseId { get; }
        }
    }
}
