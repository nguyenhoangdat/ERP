using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveStockTakingItemToBinCommand : IRequest<StockTaking.Item>
    {
        public MoveStockTakingItemToBinCommand(long positionId, int stockTakingId)
        {
            this.PositionId = positionId;
            this.StockTakingId = stockTakingId;
        }

        public long PositionId { get; set; }
        public int StockTakingId { get; set; }
    }
}
