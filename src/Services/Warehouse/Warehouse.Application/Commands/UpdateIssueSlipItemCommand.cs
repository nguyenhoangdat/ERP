using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateIssueSlipItemCommand : IRequest<IssueSlip.Item>
    {
        public UpdateIssueSlipItemCommand(int wareId, long issueSlipId, long positionId, int issuedUnits)
        {
            this.WareId = wareId;
            this.IssueSlipId = issueSlipId;
            this.PositionId = positionId;
            this.IssuedUnits = issuedUnits;
        }

        public int WareId { get; }
        public long IssueSlipId { get; }

        public long PositionId { get; }
        public int IssuedUnits { get; }
    }
}
