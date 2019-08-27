using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateStockTakingItemCurrentStockCommand : IRequest<StockTaking.Item>
    {
        public UpdateStockTakingItemCurrentStockCommand(int stockTakingId, long positionId, int currentStock)
        {
            this.StockTakingId = stockTakingId;
            this.PositionId = positionId;
            this.CurrentStock = currentStock;
        }

        public int StockTakingId { get; }
        public long PositionId { get; }
        public int CurrentStock { get; }
    }
}
