using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class DeleteReceiptCommandHandler : IRequestHandler<DeleteReceiptCommand, Receipt>
    {
        public DeleteReceiptCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Receipt> Handle(DeleteReceiptCommand request, CancellationToken cancellationToken)
        {
            Receipt receipt = this.DatabaseContext.Receipts.FirstOrDefault(x => x.Id == request.Id);
            if (receipt == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Receipt_Delete_EntityNotFoundException, request.Id));
            }

            receipt = this.DatabaseContext.Receipts.Remove(receipt).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new ReceiptDeletedDomainEvent(receipt), cancellationToken);

            return receipt;
        }
    }
}
