using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindIssueSlipsByOrderIdAndWarehouseIdCommandHandler : IRequestHandler<FindIssueSlipsByOrderIdAndWarehouseIdCommand, IEnumerable<IssueSlip>>
    {
        public FindIssueSlipsByOrderIdAndWarehouseIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IEnumerable<IssueSlip>> Handle(FindIssueSlipsByOrderIdAndWarehouseIdCommand request, CancellationToken cancellationToken)
        {
            Warehouse.Domain.Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.Where(x => x.Id == request.WarehouseId).FirstOrDefault();
            if (warehouse == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Warehouse_EntityNotFoundException"], request.WarehouseId));
            }

            return this.DatabaseContext.IssueSlips.Where(x => 
                x.OrderId == request.OrderId &&
                x.Items.FirstOrDefault() != null && // This situation should never happen
                x.Items.FirstOrDefault().Position.Section.WarehouseId == warehouse.Id).AsEnumerable();
        }
    }
}
