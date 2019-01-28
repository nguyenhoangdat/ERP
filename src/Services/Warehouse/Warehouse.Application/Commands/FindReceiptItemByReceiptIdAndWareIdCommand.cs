using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindReceiptItemByReceiptIdAndWareIdCommand : IRequest<Receipt.Item>
    {
        public FindReceiptItemByReceiptIdAndWareIdCommand(FindReceiptItemByReceiptIdAndWareIdCommandModel model)
        {
            this.Model = model;
        }
        public FindReceiptItemByReceiptIdAndWareIdCommand(long receiptId, int wareId)
            : this(new FindReceiptItemByReceiptIdAndWareIdCommandModel(receiptId, wareId)) { }

        public FindReceiptItemByReceiptIdAndWareIdCommandModel Model { get; }

        public class FindReceiptItemByReceiptIdAndWareIdCommandModel
        {
            public FindReceiptItemByReceiptIdAndWareIdCommandModel(long receiptId, int wareId)
            {
                this.ReceiptId = receiptId;
                this.WareId = wareId;
            }

            public long ReceiptId { get; }
            public int WareId { get; }
        }
    }
}
