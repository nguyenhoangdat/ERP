using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindIssueSlipByIdCommand : IRequest<IssueSlip>
    {
        public FindIssueSlipByIdCommand(long issueSlipId)
        {
            this.IssueSlipId = issueSlipId;
        }

        public long IssueSlipId { get; }
    }
}
