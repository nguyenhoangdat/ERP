using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveStockTakingToBinCommand : IRequest<StockTaking>
    {
        public MoveStockTakingToBinCommand(int stockTakingId, bool movedToBinInCascade)
        {
            this.StockTakingId = stockTakingId;
            this.MovedToBinInCascade = movedToBinInCascade;
        }

        public int StockTakingId { get; }
        public bool MovedToBinInCascade { get; }
    }
}
