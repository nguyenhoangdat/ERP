using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateStockTakingItemCommand : IRequest<StockTaking.Item>
    {
        public UpdateStockTakingItemCommand(UpdateStockTakingItemCommandModel model)
        {
            this.Model = model;
        }

        public UpdateStockTakingItemCommandModel Model { get; }

        public class UpdateStockTakingItemCommandModel
        {
            public UpdateStockTakingItemCommandModel(int stockTakingId, int wareId, long positionId, int currentStock, int countedStock, DateTime? utcCounted)
            {
                this.StockTakingId = stockTakingId;
                this.WareId = wareId;
                this.PositionId = positionId;
                this.CurrentStock = currentStock;
                this.CountedStock = countedStock;
                this.UtcCounted = utcCounted;
            }

            public int StockTakingId { get; }
            public int WareId { get; }
            public long PositionId { get; }

            public int CurrentStock { get; }
            public int CountedStock { get; }
            public DateTime? UtcCounted { get; }
        }
    }
}
