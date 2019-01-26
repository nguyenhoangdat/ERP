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
        public UpdateStockTakingItemCommand(int stockTakingId, int wareId, long positionId, int currentStock, int countedStock, int employeeId, DateTime? utcCounted)
            : this(new UpdateStockTakingItemCommandModel(stockTakingId, wareId, positionId, currentStock, countedStock, employeeId, utcCounted)) { }

        public UpdateStockTakingItemCommandModel Model { get; }

        public class UpdateStockTakingItemCommandModel
        {
            public UpdateStockTakingItemCommandModel(int stockTakingId, int wareId, long positionId, int currentStock, int countedStock, int employeeId, DateTime? utcCounted)
            {
                this.StockTakingId = stockTakingId;
                this.WareId = wareId;
                this.PositionId = positionId;
                this.EmployeeId = employeeId;
                this.CurrentStock = currentStock;
                this.CountedStock = countedStock;
                this.UtcCounted = utcCounted;
            }

            public int StockTakingId { get; }
            public int WareId { get; }
            public long PositionId { get; }
            public int EmployeeId { get; }

            public int CurrentStock { get; }
            public int CountedStock { get; }
            public DateTime? UtcCounted { get; }
        }
    }
}
