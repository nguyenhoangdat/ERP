using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RestoreStockTakingFromBinCommand : IRequest<StockTaking>
    {
        public RestoreStockTakingFromBinCommand(int stockTakingId)
        {
            this.StockTakingId = stockTakingId;
        }

        public int StockTakingId { get; }
    }
}
