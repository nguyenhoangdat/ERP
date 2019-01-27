using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindIssueSlipToProcessInSectionCommand : IRequest<IssueSlip>
    {
        public FindIssueSlipToProcessInSectionCommand(FindIssueSlipToProcessInSectionCommandModel model)
        {
            this.Model = model;
        }
        public FindIssueSlipToProcessInSectionCommand(int sectionId)
            : this(new FindIssueSlipToProcessInSectionCommandModel(sectionId)) { }

        public FindIssueSlipToProcessInSectionCommandModel Model { get; }

        public class FindIssueSlipToProcessInSectionCommandModel
        {
            public FindIssueSlipToProcessInSectionCommandModel(int sectionId)
            {
                this.SectionId = sectionId;
            }

            public int SectionId { get; }
        }
    }
}
