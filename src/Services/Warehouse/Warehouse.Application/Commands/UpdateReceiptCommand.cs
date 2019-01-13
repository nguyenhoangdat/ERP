using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateReceiptCommand : IRequest<Receipt>
    {
        public UpdateReceiptCommand(UpdateReceiptCommandModel model)
        {
            this.Model = model;
        }

        public UpdateReceiptCommandModel Model { get; }

        public class UpdateReceiptCommandModel
        {
            public long Id { get; }
            public string Name { get; }
            public DateTime UtcExpected { get; }
            public DateTime? UtcReceived { get; }
            public List<Item> Items { get; }

            public class Item
            {
                public int WareId { get; }
                public long PositionId { get; }
                public long ReceiptId { get; }

                public int CountOrdered { get; }
                public int CountReceived { get; }
                public DateTime? UtcProcessed { get; }
                public int EmployeeId { get; }
            }
        }
    }
}
