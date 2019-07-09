using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindIssueSlipItemToProcessInSectionByIssueSlipIdCommand : IRequest<IssueSlip.Item>
    {
        public FindIssueSlipItemToProcessInSectionByIssueSlipIdCommand(int sectionId, long issueSlipId)
        {
            this.SectionId = sectionId;
            this.IssueSlipId = issueSlipId;
        }

        public int SectionId { get; }
        public long IssueSlipId { get; }
    }
}
