using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveIssueSlipItemToBinCommand : IRequest<IssueSlip.Item>
    {
        public MoveIssueSlipItemToBinCommand(long issueSlipId, long? positionId, int wareId, bool movedToBinInCascade)
        {
            this.IssueSlipId = issueSlipId;
            this.PositionId = positionId;
            this.WareId = wareId;
            this.MovedToBinInCascade = movedToBinInCascade;
        }

        public long IssueSlipId { get; }
        public long? PositionId { get; }
        public int WareId { get; }
        public bool MovedToBinInCascade { get; }
    }
}
