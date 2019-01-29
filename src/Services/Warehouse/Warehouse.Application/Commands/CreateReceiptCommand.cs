using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateReceiptCommand : IRequest<Receipt>
    {
        public CreateReceiptCommand(CreateReceiptCommandModel model)
        {
            this.Model = model;
        }
        public CreateReceiptCommand(DateTime utcExpected, List<CreateReceiptCommandModel.Item> items)
            : this(new CreateReceiptCommandModel(utcExpected, items)) { }

        public CreateReceiptCommandModel Model { get; }

        public class CreateReceiptCommandModel
        {
            public CreateReceiptCommandModel(DateTime utcExpected, List<Item> items)
            {
                this.UtcExpected = utcExpected;
                this.Items = items;
            }

            public DateTime UtcExpected { get; }
            public List<Item> Items { get; }

            public class Item
            {
                public Item(int wareId, int countOrdered)
                {
                    this.WareId = wareId;
                    this.CountOrdered = countOrdered;
                }

                public int WareId { get; }
                public int CountOrdered { get; }
            }
        }
    }
}
