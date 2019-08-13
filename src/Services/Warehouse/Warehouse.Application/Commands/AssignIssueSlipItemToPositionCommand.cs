using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class AssignIssueSlipItemToPositionCommand : IRequest<IssueSlip.Item>
    {
        public AssignIssueSlipItemToPositionCommand(long issueSlipId, long positionId, int wareId)
        {
            this.IssueSlipId = issueSlipId;
            this.WareId = wareId;
            this.PositionId = positionId;
        }

        public long IssueSlipId { get; }
        public int WareId { get; }
        public long PositionId { get; }
    }
}
