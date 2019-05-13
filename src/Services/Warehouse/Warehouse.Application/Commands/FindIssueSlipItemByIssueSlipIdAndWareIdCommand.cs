using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindIssueSlipItemByIssueSlipIdAndWareIdCommand : IRequest<IssueSlip.Item>
    {
        public FindIssueSlipItemByIssueSlipIdAndWareIdCommand(long issueSlipId, int wareId)
        {
            this.IssueSlipId = issueSlipId;
            this.WareId = wareId;
        }

        public long IssueSlipId { get; }
        public int WareId { get; }
    }
}
