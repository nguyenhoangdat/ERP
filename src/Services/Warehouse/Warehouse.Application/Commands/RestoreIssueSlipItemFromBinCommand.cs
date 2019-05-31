using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RestoreIssueSlipItemFromBinCommand : IRequest<IssueSlip.Item>
    {
        public RestoreIssueSlipItemFromBinCommand(int wareId, long issueSlipId)
        {
            this.WareId = wareId;
            this.IssueSlipId = issueSlipId;
        }

        public int WareId { get; }
        public long IssueSlipId { get; }
    }
}
