using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class MoveReceiptToBinCommandHandler : IRequestHandler<MoveReceiptToBinCommand, Receipt>
    {
        public MoveReceiptToBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Receipt> Handle(MoveReceiptToBinCommand request, CancellationToken cancellationToken)
        {
            Receipt receipt = this.DatabaseContext.Receipts.FirstOrDefault(x => x.Id == request.ReceiptId);

            if (receipt == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Receipt_EntityNotFoundException, request.ReceiptId));
            }
            if (request.MovedToBinInCascade == false)
            {
                if (receipt.CanBeMovedToBin() == false)
                {
                    throw new EntityMoveToBinException(string.Format(Properties.Resources.ReceiptItem_EntityMoveToBinException, request.ReceiptId));
                }
            }            

            receipt.UtcMovedToBin = DateTime.UtcNow;
            receipt.MovedToBinInCascade = request.MovedToBinInCascade;

            foreach (Receipt.Item item in receipt.Items.Where(x => x.UtcMovedToBin == null))
            {
                await this.Mediator.Send(new MoveReceiptItemToBinCommand(item.ReceiptId, item.PositionId, item.WareId, true), cancellationToken);
            }

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return receipt;
        }
    }
}
