using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveStockTakingItemToBinCommand : IRequest<StockTaking.Item>
    {
        public MoveStockTakingItemToBinCommand(int stockTakingId, long positionId, bool movedToBinInCascade)
        {
            this.StockTakingId = stockTakingId;
            this.PositionId = positionId;
            this.MovedToBinInCascade = movedToBinInCascade;
        }

        public int StockTakingId { get; }
        public long PositionId { get; }
        public bool MovedToBinInCascade { get; }
    }
}
