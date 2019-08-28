using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RestoreStockTakingItemFromBinCommand : IRequest<StockTaking.Item>
    {
        public RestoreStockTakingItemFromBinCommand(int stockTakingId, long positionId)
        {
            this.StockTakingId = stockTakingId;
            this.PositionId = positionId;
        }

        public long PositionId { get; }
        public int StockTakingId { get; }
    }
}
