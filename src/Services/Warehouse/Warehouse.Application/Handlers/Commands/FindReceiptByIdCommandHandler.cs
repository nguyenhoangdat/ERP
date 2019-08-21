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
    public class FindReceiptByIdCommandHandler : IRequestHandler<FindReceiptByIdCommand, Receipt>
    {
        public FindReceiptByIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Receipt> Handle(FindReceiptByIdCommand request, CancellationToken cancellationToken)
        {
            Receipt receipt = this.DatabaseContext.Receipts.FirstOrDefault(x => x.Id == request.ReceiptId);
            if (receipt == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Receipt_EntityNotFoundException"], request.ReceiptId));
            }

            return receipt;
        }
    }
}
