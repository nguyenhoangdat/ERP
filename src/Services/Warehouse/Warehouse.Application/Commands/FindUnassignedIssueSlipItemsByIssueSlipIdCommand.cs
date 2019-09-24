using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindUnassignedIssueSlipItemsByIssueSlipIdCommand : IRequest<IEnumerable<IssueSlip.Item>>
    {
        public FindUnassignedIssueSlipItemsByIssueSlipIdCommand(long issueSlipId)
        {
            this.IssueSlipId = issueSlipId;
        }

        public long IssueSlipId { get; }
    }
}
