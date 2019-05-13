using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindIssueSlipToProcessInSectionCommand : IRequest<IssueSlip>
    {
        public FindIssueSlipToProcessInSectionCommand(int sectionId)
        {
            this.SectionId = sectionId;
        }

        public int SectionId { get; }
    }
}
