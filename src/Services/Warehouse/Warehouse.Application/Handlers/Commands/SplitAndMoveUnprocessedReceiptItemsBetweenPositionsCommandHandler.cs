using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class SplitAndMoveUnprocessedReceiptItemsBetweenPositionsCommandHandler : IRequestHandler<SplitAndMoveUnprocessedReceiptItemsBetweenPositionsCommand, IEnumerable<Receipt.Item>>
    {
        public SplitAndMoveUnprocessedReceiptItemsBetweenPositionsCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IEnumerable<Receipt.Item>> Handle(SplitAndMoveUnprocessedReceiptItemsBetweenPositionsCommand request, CancellationToken cancellationToken)
        {
            List<Receipt.Item> dbEntities = this.DatabaseContext.ReceiptItems.Where(x => x.PositionId == request.FromPositionId && x.CountReceived < x.CountOrdered).ToList();
            List<Receipt.Item> output = new List<Receipt.Item>(dbEntities.Count * 2);

            foreach (Receipt.Item item in dbEntities)
            {
                int diff = item.CountOrdered - item.CountReceived;

                output.Add(await this.Mediator.Send(new UpdateReceiptItemOrderedUnitsCommand(item.ReceiptId, item.PositionId, item.WareId, item.CountReceived), cancellationToken));
                output.Add(await this.Mediator.Send(new CreateReceiptItemCommand(item.WareId, request.ToPositionId, item.ReceiptId, diff, 0)));
            }

            return output;
        }
    }
}
