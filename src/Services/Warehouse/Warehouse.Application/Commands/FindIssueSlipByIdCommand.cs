using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindIssueSlipByIdCommand : IRequest<IssueSlip>
    {
        public FindIssueSlipByIdCommand(FindIssueSlipByIdCommandModel model)
        {
            this.Model = model;
        }
        public FindIssueSlipByIdCommand(long issueSlipId)
            : this(new FindIssueSlipByIdCommandModel(issueSlipId)) { }

        public FindIssueSlipByIdCommandModel Model { get; }

        public class FindIssueSlipByIdCommandModel
        {
            public FindIssueSlipByIdCommandModel(long issueSlipId)
            {
                this.IssueSlipId = issueSlipId;
            }

            public long IssueSlipId { get; }
        }
    }
}
