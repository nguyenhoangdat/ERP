using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class SplitAndMoveUnprocessedIssueSlipItemsBetweenPositionsCommand : IRequest<IEnumerable<IssueSlip.Item>>
    {
        public SplitAndMoveUnprocessedIssueSlipItemsBetweenPositionsCommand(long fromPositionId, long toPositionId)
        {
            if (fromPositionId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(fromPositionId));
            }
            this.FromPositionId = fromPositionId;

            if (toPositionId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(toPositionId));
            }
            this.ToPositionId = toPositionId;
        }

        public long FromPositionId { get; }
        public long ToPositionId { get; }
    }
}
