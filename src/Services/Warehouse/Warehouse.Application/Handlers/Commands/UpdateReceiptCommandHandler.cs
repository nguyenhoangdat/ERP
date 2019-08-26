using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateReceiptCommandHandler : IRequestHandler<UpdateReceiptCommand, Receipt>
    {
        public UpdateReceiptCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Receipt> Handle(UpdateReceiptCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Receipts.Any(x => x.Id == request.Id))
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Receipt_Update_EntityNotFoundException, request.Id));
            }

            Receipt receipt = this.DatabaseContext.Receipts.FirstOrDefault(x => x.Id == request.Id);
            receipt.UtcExpected = request.UtcExpected;
            receipt.UtcReceived = request.UtcReceived;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new ReceiptUpdatedDomainEvent(receipt), cancellationToken);

            return receipt;
        }
    }
}