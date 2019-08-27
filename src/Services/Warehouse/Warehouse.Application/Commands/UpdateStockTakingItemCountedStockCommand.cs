using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateStockTakingItemCountedStockCommand : IRequest<StockTaking.Item>
    {
        public UpdateStockTakingItemCountedStockCommand(int stockTakingId, long positionId, int countedStock)
        {
            this.StockTakingId = stockTakingId;
            this.PositionId = positionId;
            this.CountedStock = countedStock;
        }

        public int StockTakingId { get; }
        public long PositionId { get; }
        public int CountedStock { get; }
    }
}
