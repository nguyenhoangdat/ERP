using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class AssignIssueSlipItemToPositionCommand : IRequest<IssueSlip.Item>
    {
        public AssignIssueSlipItemToPositionCommand(long issueSlipId, long positionId, int wareId)
        {
            this.IssueSlipId = issueSlipId;
            this.WareId = wareId;

            if (positionId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(positionId));
            }
            this.PositionId = positionId;
        }

        public long IssueSlipId { get; }
        public int WareId { get; }
        public long PositionId { get; }
    }
}
