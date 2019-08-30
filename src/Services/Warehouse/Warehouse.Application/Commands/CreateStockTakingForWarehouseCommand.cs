using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateStockTakingForWarehouseCommand : IRequest<StockTaking>
    {
        public CreateStockTakingForWarehouseCommand(int warehouseId, bool includeEmptyPositions)
        {
            this.WarehouseId = warehouseId;
            this.IncludeEmptyPositions = includeEmptyPositions;
        }

        public int WarehouseId { get; }
        public bool IncludeEmptyPositions { get; }
    }
}
