using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class SplitAndMoveUnprocessedIssueSlipItemsBetweenPositionsCommand : IRequest<IEnumerable<IssueSlip.Item>>
    {
        public SplitAndMoveUnprocessedIssueSlipItemsBetweenPositionsCommand(long fromPositionId, long toPositionId)
        {
            this.FromPositionId = fromPositionId;
            this.ToPositionId = toPositionId;
        }

        public long FromPositionId { get; }
        public long ToPositionId { get; }
    }
}
