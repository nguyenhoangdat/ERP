using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveIssueSlipToBinCommand : IRequest<IssueSlip>
    {
        public MoveIssueSlipToBinCommand(long issueSlipId)
        {
            this.IssueSlipId = issueSlipId;
        }

        public long IssueSlipId { get; set; }
    }
}
