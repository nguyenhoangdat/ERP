using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateStockTakingCommand : IRequest<StockTaking>
    {
        public UpdateStockTakingCommand(int id, string name, List<Item> items)
        {
            this.Id = id;
            this.Name = name;
            this.Items = items;
        }

        public int Id { get; }
        public string Name { get; }
        public List<Item> Items { get; }

        public class Item
        {
            public Item(int stockTakingId, int wareId, long positionId, int currentStock, int countedStock, DateTime? utcCounted)
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
