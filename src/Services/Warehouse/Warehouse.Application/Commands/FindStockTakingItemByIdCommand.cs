using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindStockTakingItemByIdCommand : IRequest<StockTaking.Item>
    {
        public FindStockTakingItemByIdCommand(int stockTakingId, long positionId)
        {
            this.StockTakingId = stockTakingId;
            this.PositionId = positionId;
        }

        public int StockTakingId { get; }
        public long PositionId { get; }
    }
}
