using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RestoreStockTakingItemFromBinCommand : IRequest<StockTaking.Item>
    {
        public RestoreStockTakingItemFromBinCommand(long positionId, int stockTakingId)
        {
            this.PositionId = positionId;
            this.StockTakingId = stockTakingId;
        }

        public long PositionId { get; }
        public int StockTakingId { get; }
    }
}
