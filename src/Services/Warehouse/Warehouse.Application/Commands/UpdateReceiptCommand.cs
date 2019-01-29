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
        public UpdateReceiptCommand(long id, DateTime utcExpected, DateTime? utcReceived, List<UpdateReceiptCommandModel.Item> items)
            : this(new UpdateReceiptCommandModel(id, utcExpected, utcReceived, items)) { }

        public UpdateReceiptCommandModel Model { get; }

        public class UpdateReceiptCommandModel
        {
            public UpdateReceiptCommandModel(long id, DateTime utcExpected, DateTime? utcReceived, List<Item> items)
            {
                this.Id = id;
                this.UtcExpected = utcExpected;
                this.UtcReceived = utcReceived;
                this.Items = items;
            }

            public long Id { get; }
            public DateTime UtcExpected { get; }
            public DateTime? UtcReceived { get; }
            public List<Item> Items { get; }

            public class Item
            {
                public Item(int wareId, long? positionId, long receiptId, int countOrdered, int countReceived, DateTime? utcProcessed, int employeeId)
                {
                    this.WareId = wareId;
                    this.PositionId = positionId;
                    this.ReceiptId = receiptId;
                    this.CountOrdered = countOrdered;
                    this.CountReceived = countReceived;
                    this.UtcProcessed = utcProcessed;
                    this.EmployeeId = employeeId;
                }

                public int WareId { get; }
                public long? PositionId { get; }
                public long ReceiptId { get; }

                public int CountOrdered { get; }
                public int CountReceived { get; }
                public DateTime? UtcProcessed { get; }
                public int EmployeeId { get; }
            }
        }
    }
}
