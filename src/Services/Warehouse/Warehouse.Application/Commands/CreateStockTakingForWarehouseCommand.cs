using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateStockTakingForWarehouseCommand : IRequest<StockTaking>
    {
        public CreateStockTakingForWarehouseCommand(CreateStockTakingForWarehouseCommandModel model)
        {
            this.Model = model;
        }
        public CreateStockTakingForWarehouseCommand(int warehouseId)
            : this(new CreateStockTakingForWarehouseCommandModel(warehouseId)) { }

        public CreateStockTakingForWarehouseCommandModel Model { get; }

        public class CreateStockTakingForWarehouseCommandModel
        {
            public CreateStockTakingForWarehouseCommandModel(int warehouseId)
            {
                this.WarehouseId = warehouseId;
            }
            public int WarehouseId { get; }
        }
    }
}
