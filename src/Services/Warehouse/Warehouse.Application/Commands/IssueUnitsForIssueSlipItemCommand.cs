using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class IssueUnitsForIssueSlipItemCommand : IRequest<IssueSlip.Item>
    {
        public IssueUnitsForIssueSlipItemCommand(long issueSlipId, int wareId, long positionId, int count)
        {
            this.IssueSlipId = issueSlipId;
            this.WareId = wareId;
            this.PositionId = positionId;
            this.Count = count;
        }

        public long IssueSlipId { get; }
        public int WareId { get; }
        public long PositionId { get; }
        public int Count { get; }
    }
}
