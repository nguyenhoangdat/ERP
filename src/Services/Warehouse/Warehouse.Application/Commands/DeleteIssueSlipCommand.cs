using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteIssueSlipCommand : IRequest<IssueSlip>
    {
        public DeleteIssueSlipCommand(DeleteIssueSlipCommandModel model)
        {
            this.Model = model;
        }
        public DeleteIssueSlipCommand(long issueSlipId)
            : this(new DeleteIssueSlipCommandModel(issueSlipId)) { }

        public DeleteIssueSlipCommandModel Model { get; }

        public class DeleteIssueSlipCommandModel
        {
            public DeleteIssueSlipCommandModel(long id)
            {
                this.Id = id;
            }

            public long Id { get; }
        }
    }
}
