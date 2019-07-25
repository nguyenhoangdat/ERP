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
    public class SplitAndMoveUnprocessedIssueSlipItemsBetweenPositionsCommandHandler : IRequestHandler<SplitAndMoveUnprocessedIssueSlipItemsBetweenPositionsCommand, IEnumerable<IssueSlip.Item>>
    {
        public SplitAndMoveUnprocessedIssueSlipItemsBetweenPositionsCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IEnumerable<IssueSlip.Item>> Handle(SplitAndMoveUnprocessedIssueSlipItemsBetweenPositionsCommand request, CancellationToken cancellationToken)
        {
            List<IssueSlip.Item> dbEntities = this.DatabaseContext.IssueSlipItems.Where(x => x.PositionId == request.FromPositionId && x.IssuedUnits < x.RequestedUnits).ToList();
            List<IssueSlip.Item> output = new List<IssueSlip.Item>(dbEntities.Count * 2);

            foreach (IssueSlip.Item item in dbEntities)
            {
                int diff = item.RequestedUnits - item.IssuedUnits;

                output.Add(await this.Mediator.Send(new UpdateIssueSlipItemRequestedUnitsCommand(item.IssueSlipId, item.WareId, item.RequestedUnits - diff), cancellationToken));
                output.Add(await this.Mediator.Send(new CreateIssueSlipItemCommand(item.IssueSlipId, item.WareId, request.ToPositionId, diff, 0)));
            }

            return output;
        }
    }
}
