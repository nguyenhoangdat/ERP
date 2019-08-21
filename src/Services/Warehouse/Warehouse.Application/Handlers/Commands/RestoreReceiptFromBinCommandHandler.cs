using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RestoreReceiptFromBinCommandHandler : IRequestHandler<RestoreReceiptFromBinCommand, Receipt>
    {
        public RestoreReceiptFromBinCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Receipt> Handle(RestoreReceiptFromBinCommand request, CancellationToken cancellationToken)
        {
            Receipt receipt = this.DatabaseContext.Receipts.FirstOrDefault(x => x.Id == request.ReceiptId);

            if (receipt == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Receipt_EntityNotFoundException"], request.ReceiptId));
            }

            receipt.UtcMovedToBin = null;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return receipt;
        }
    }
}
