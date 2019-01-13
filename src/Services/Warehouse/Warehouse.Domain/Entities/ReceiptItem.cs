using Restmium.ERP.Services.Warehouse.Domain.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public partial class Receipt
    {
        public class Item : WarePosition
        {
            public Item()
            {

            }
            public Item(long receiptId, long positionId, int wareId, int countOrdered, int countReceived, int employeeId) : this()
            {
                this.ReceiptId = receiptId;
                this.PositionId = positionId;
                this.WareId = wareId;
                this.CountOrdered = countOrdered;
                this.CountReceived = countReceived;
                this.EmployeeId = employeeId;
            }
            public Item(Receipt receipt, long positionId, int wareId, int countOrdered, int countReceived, int employeeId) : this(receipt.Id, positionId, wareId, countOrdered, countReceived, employeeId)
            {
                this.Receipt = receipt;
            }
            public Item(long receiptId, long positionId, int wareId, int countOrdered, int countReceived, int employeeId, DateTime? utcProcessed) : this(receiptId, positionId, wareId, countOrdered, countReceived, employeeId)
            {
                this.UtcProcessed = utcProcessed;
            }
            public Item(Receipt receipt, long positionId, int wareId, int countOrdered, int countReceived, int employeeId, DateTime? utcProcessed) : this(receipt, positionId, wareId, countOrdered, countReceived, employeeId)
            {
                this.UtcProcessed = utcProcessed;
            }

            [Required]
            public long ReceiptId { get; set; }
            public virtual Receipt Receipt { get; set; }

            [Required]
            public int CountOrdered { get; set; }
            [Required]
            public int CountReceived { get; set; }

            public DateTime? UtcProcessed { get; set; }

            /// <summary>
            /// FK to Employees.API
            /// </summary>
            [Required]
            public int EmployeeId { get; set; }
        }
    }
}
