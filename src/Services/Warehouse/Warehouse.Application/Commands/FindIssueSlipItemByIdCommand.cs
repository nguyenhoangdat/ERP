using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindIssueSlipItemByIdCommand : IRequest<IssueSlip.Item>
    {
        public FindIssueSlipItemByIdCommand(long issueSlipId, long positionId, int wareId)
        {
            this.IssueSlipId = issueSlipId;
            this.PositionId = positionId;
            this.WareId = wareId;
        }

        public long IssueSlipId { get; }
        public long PositionId { get; }
        public int WareId { get; }
    }
}
