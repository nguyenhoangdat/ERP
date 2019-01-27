using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindReceiptByIdCommand : IRequest<Receipt>
    {
        public FindReceiptByIdCommand(FindReceiptByIdCommandModel model)
        {
            this.Model = model;
        }
        public FindReceiptByIdCommand(long receiptId)
            : this(new FindReceiptByIdCommandModel(receiptId)) { }

        public FindReceiptByIdCommandModel Model { get; }

        public class FindReceiptByIdCommandModel
        {
            public FindReceiptByIdCommandModel(long receiptId)
            {
                this.ReceiptId = receiptId;
            }

            public long ReceiptId { get; }
        }
    }
}
