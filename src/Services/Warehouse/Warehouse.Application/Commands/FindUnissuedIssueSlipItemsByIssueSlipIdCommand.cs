
using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindUnissuedIssueSlipItemsByIssueSlipIdCommand : IRequest<IEnumerable<IssueSlip.Item>>
    {
        public FindUnissuedIssueSlipItemsByIssueSlipIdCommand(long issueSlipId)
        {
            this.IssueSlipId = issueSlipId;
        }

        public long IssueSlipId { get; }
    }
}
