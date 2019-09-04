using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateIssueSlipItemRequestedUnitsCommand : IRequest<IssueSlip.Item>
    {
        public UpdateIssueSlipItemRequestedUnitsCommand(long issueSlipId, long positionId, int wareId, int requestedUnits)
        {
            this.IssueSlipId = issueSlipId;
            this.PositionId = positionId;
            this.WareId = wareId;
            this.RequestedUnits = requestedUnits;
        }

        public long IssueSlipId { get; }
        public long PositionId { get; }
        public int WareId { get; }
        public int RequestedUnits { get; }
    }
}
