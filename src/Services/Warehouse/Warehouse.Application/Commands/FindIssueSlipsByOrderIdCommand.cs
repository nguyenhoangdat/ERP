using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindIssueSlipsByOrderIdCommand : IRequest<IEnumerable<IssueSlip>>
    {
        public FindIssueSlipsByOrderIdCommand(long orderId)
        {
            this.OrderId = orderId;
        }

        public long OrderId { get; }
    }
}
