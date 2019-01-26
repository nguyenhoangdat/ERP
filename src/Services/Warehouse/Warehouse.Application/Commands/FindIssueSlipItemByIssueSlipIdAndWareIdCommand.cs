using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindIssueSlipItemByIssueSlipIdAndWareIdCommand : IRequest<IssueSlip.Item>
    {
        public FindIssueSlipItemByIssueSlipIdAndWareIdCommand(FindIssueSlipItemByIssueSlipIdAndWareIdCommandModel model)
        {
            this.Model = model;
        }
        public FindIssueSlipItemByIssueSlipIdAndWareIdCommand(long issueSlipId, int wareId)
            : this(new FindIssueSlipItemByIssueSlipIdAndWareIdCommandModel(issueSlipId, wareId)) { }

        public FindIssueSlipItemByIssueSlipIdAndWareIdCommandModel Model { get; }

        public class FindIssueSlipItemByIssueSlipIdAndWareIdCommandModel
        {
            public FindIssueSlipItemByIssueSlipIdAndWareIdCommandModel(long issueSlipId, int wareId)
            {
                this.IssueSlipId = issueSlipId;
                this.WareId = wareId;
            }

            public long IssueSlipId { get; }
            public int WareId { get; }
        }
    }
}
