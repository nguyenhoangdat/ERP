using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateReceiptCommand : IRequest<Receipt>
    {
        public UpdateReceiptCommand(long id, DateTime utcExpected, DateTime? utcReceived, List<Item> items)
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
            public Item(int wareId, long? positionId, long receiptId, int countOrdered, int countReceived, DateTime? utcProcessed)
            {
                this.WareId = wareId;
                this.PositionId = positionId;
                this.ReceiptId = receiptId;
                this.CountOrdered = countOrdered;
                this.CountReceived = countReceived;
                this.UtcProcessed = utcProcessed;
            }

            public int WareId { get; }
            public long? PositionId { get; }
            public long ReceiptId { get; }

            public int CountOrdered { get; }
            public int CountReceived { get; }
            public DateTime? UtcProcessed { get; }
        }
    }
}
