using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveIssueSlipItemToBinCommand : IRequest<IssueSlip.Item>
    {
        public MoveIssueSlipItemToBinCommand(int wareId, long issueSlipId)
        {
            this.WareId = wareId;
            this.IssueSlipId = issueSlipId;
        }

        public int WareId { get; set; }
        public long IssueSlipId { get; set; }
    }
}
