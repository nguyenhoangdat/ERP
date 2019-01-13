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

        public CreateReceiptCommandModel Model { get; }

        public class CreateReceiptCommandModel
        {
            public string Name { get; }
            public DateTime UtcExpected { get; }
            public List<Item> Items { get; }

            public class Item
            {
                public int WareId { get; }
                public long PositionId { get; }
                public int CountOrdered { get; }
            }
        }
    }
}
