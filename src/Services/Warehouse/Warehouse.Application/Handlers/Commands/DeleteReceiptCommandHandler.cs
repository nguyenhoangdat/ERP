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
        //TODO: Check grammar
        protected const string DeleteWareCommandHandlerEntityNotFoundException = "Unable to delete Receipt with id={0}. Receipt not found!";

        public DeleteReceiptCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Receipt> Handle(DeleteReceiptCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Receipts.Any(x => x.Id == request.Model.Id))
            {
                throw new EntityNotFoundException(string.Format(DeleteWareCommandHandlerEntityNotFoundException, request.Model.Id));
            }

            Receipt receipt = this.DatabaseContext.Receipts.Remove(this.DatabaseContext.Receipts.Find(request.Model.Id)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new ReceiptDeletedDomainEvent(receipt));

            return receipt;
        }
    }
}
