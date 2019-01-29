using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateReceiptItemCommand : IRequest<Receipt.Item>
    {
        public UpdateReceiptItemCommand(UpdateReceiptItemCommandModel model)
        {
            this.Model = model;
        }
        public UpdateReceiptItemCommand(int wareId, long? positionId, long receiptId, int countOrdered, int countReceived, DateTime? utcProcessed, int employeeId)
            : this(new UpdateReceiptItemCommandModel(wareId, positionId, receiptId, countOrdered, countReceived, utcProcessed, employeeId)) { }

        public UpdateReceiptItemCommandModel Model { get; }

        public class UpdateReceiptItemCommandModel
        {
            public UpdateReceiptItemCommandModel(int wareId, long? positionId, long receiptId, int countOrdered, int countReceived, DateTime? utcProcessed, int employeeId)
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
