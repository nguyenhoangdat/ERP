using Restmium.ERP.BuildingBlocks.Common.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public partial class Receipt
    {
        public class Item : DatabaseEntity
        {
            public Item()
            {

            }
            public Item(int wareId, int countOrdered) : this()
            {
                this.WareId = wareId;
                this.PositionId = 1;
                this.CountOrdered = countOrdered;
            }
            public Item(long receiptId, long positionId, int wareId, int countOrdered, int countReceived) : this(wareId, countOrdered)
            {
                this.ReceiptId = receiptId;
                this.PositionId = positionId;
                this.CountReceived = countReceived;
            }
            public Item(Receipt receipt, long positionId, int wareId, int countOrdered, int countReceived) : this(receipt.Id, positionId, wareId, countOrdered, countReceived)
            {
                this.Receipt = receipt;
            }
            public Item(long receiptId, long positionId, int wareId, int countOrdered, int countReceived, DateTime? utcProcessed) : this(receiptId, positionId, wareId, countOrdered, countReceived)
            {
                this.UtcProcessed = utcProcessed;
            }
            public Item(Receipt receipt, long positionId, int wareId, int countOrdered, int countReceived, DateTime? utcProcessed) : this(receipt, positionId, wareId, countOrdered, countReceived)
            {
                this.UtcProcessed = utcProcessed;
            }

            [Required]
            public int WareId { get; set; }
            public virtual Ware Ware { get; set; }

            [Required]
            public long PositionId { get; set; }
            public virtual Position Position { get; set; }

            [Required]
            public long ReceiptId { get; set; }
            public virtual Receipt Receipt { get; set; }

            [Required]
            public int CountOrdered { get; set; }
            [Required]
            public int CountReceived { get; set; }

            public DateTime? UtcProcessed { get; set; }
        }
    }
}
