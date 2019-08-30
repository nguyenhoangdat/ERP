using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateStockTakingCommand : IRequest<StockTaking>
    {
        public CreateStockTakingCommand(string name, List<Item> items)
        {
            this.Name = name;
            this.Items = items;
        }

        public string Name { get; }
        public List<Item> Items { get; }

        public class Item
        {
            public Item(int? wareId, long positionId, int currentStock, int countedStock)
            {
                this.WareId = wareId;
                this.PositionId = positionId;
                this.CurrentStock = currentStock;
                this.CountedStock = countedStock;
            }

            public int? WareId { get; }
            public long PositionId { get; }
            public int CurrentStock { get; }
            public int CountedStock { get; }
        }
    }
}
