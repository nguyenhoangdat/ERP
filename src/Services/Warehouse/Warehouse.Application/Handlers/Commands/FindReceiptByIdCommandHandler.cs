using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindReceiptByIdCommandHandler : IRequestHandler<FindReceiptByIdCommand, Receipt>
    {
        public FindReceiptByIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Receipt> Handle(FindReceiptByIdCommand request, CancellationToken cancellationToken)
        {
            Receipt receipt = await this.DatabaseContext.Receipts.FindAsync(new object[] { request.Model.ReceiptId }, cancellationToken);
            if (receipt == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Receipt_EntityNotFoundException"], request.Model.ReceiptId));
            }

            return receipt;
        }
    }
}
