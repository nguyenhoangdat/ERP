using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RestoreIssueSlipFromBinCommand : IRequest<IssueSlip>
    {
        public RestoreIssueSlipFromBinCommand(long issueSlipId)
        {
            this.IssueSlipId = issueSlipId;
        }

        public long IssueSlipId { get; }
    }
}
