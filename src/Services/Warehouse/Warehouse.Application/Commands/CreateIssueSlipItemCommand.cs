using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateIssueSlipItemCommand : IRequest<IssueSlip.Item>
    {
        public CreateIssueSlipItemCommand(long issueSlipId, int wareId, long positionId, int requestedUnits, int issuedUnits)
        {
            this.IssueSlipId = issueSlipId;
            this.WareId = wareId;
            this.PositionId = positionId;
            this.RequestedUnits = requestedUnits;
            this.IssuedUnits = issuedUnits;
        }

        public long IssueSlipId { get; }
        public int WareId { get; }
        public long PositionId { get; }
        public int RequestedUnits { get; }
        public int IssuedUnits { get; }
    }
}
