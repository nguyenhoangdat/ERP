using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class GetWarehouseCurrentCapacityCommand : IRequest<WarehouseCapacityDTO>
    {
        public GetWarehouseCurrentCapacityCommand(GetWarehouseCurrentCapacityCommandModel model)
        {
            this.Model = model;
        }
        public GetWarehouseCurrentCapacityCommand(int warehouseId) : this(new GetWarehouseCurrentCapacityCommandModel(warehouseId)) { }

        public GetWarehouseCurrentCapacityCommandModel Model { get; }

        public class GetWarehouseCurrentCapacityCommandModel
        {
            public GetWarehouseCurrentCapacityCommandModel(int warehouseId)
            {
                this.WarehouseId = warehouseId;
            }

            public int WarehouseId { get; }
        }
    }
}
