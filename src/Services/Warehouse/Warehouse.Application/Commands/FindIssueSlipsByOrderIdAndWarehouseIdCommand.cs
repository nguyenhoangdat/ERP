using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindIssueSlipsByOrderIdAndWarehouseIdCommand : IRequest<IEnumerable<IssueSlip>>
    {
        public FindIssueSlipsByOrderIdAndWarehouseIdCommand(long orderId, int warehouseId)
        {
            this.OrderId = orderId;
            this.WarehouseId = warehouseId;
        }

        public long OrderId { get; }
        public int WarehouseId { get; }
    }
}
