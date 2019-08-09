using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveIssueSlipItemToBinCommand : IRequest<IssueSlip.Item>
    {
        public MoveIssueSlipItemToBinCommand(long issueSlipId, long positionId, int wareId)
        {
            this.IssueSlipId = issueSlipId;
            this.PositionId = positionId;
            this.WareId = wareId;
        }

        public int WareId { get; set; }
        public long IssueSlipId { get; set; }
        public long PositionId { get; }
    }
}
