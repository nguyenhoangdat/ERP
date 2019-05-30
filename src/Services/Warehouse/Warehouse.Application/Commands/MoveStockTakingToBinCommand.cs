using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveStockTakingToBinCommand : IRequest<StockTaking>
    {
        public MoveStockTakingToBinCommand(int stockTakingId)
        {
            this.StockTakingId = stockTakingId;
        }

        public int StockTakingId { get; set; }
    }
}
