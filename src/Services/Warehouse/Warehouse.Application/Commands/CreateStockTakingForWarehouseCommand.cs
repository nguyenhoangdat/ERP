using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateStockTakingForWarehouseCommand : IRequest<StockTaking>
    {
        public CreateStockTakingForWarehouseCommand(int warehouseId)
        {
            this.WarehouseId = warehouseId;
        }

        public int WarehouseId { get; }
    }
}
