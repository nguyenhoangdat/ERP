using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateReceiptItemCommandHandler : IRequestHandler<UpdateReceiptItemCommand, Receipt.Item>
    {
        public UpdateReceiptItemCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Receipt.Item> Handle(UpdateReceiptItemCommand request, CancellationToken cancellationToken)
        {
            Receipt.Item item = await this.DatabaseContext.ReceiptItems.FindAsync(new object[] { request.ReceiptId, request.WareId }, cancellationToken);
            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["ReceiptItem_Update_EntityNotFoundException"], request.ReceiptId, request.WareId));
            }

            return item;
        }
    }
}
