using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class GetWarehouseCurrentCapacityCommandHandler : IRequestHandler<GetWarehouseCurrentCapacityCommand, WarehouseCapacity>
    {
        public GetWarehouseCurrentCapacityCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<WarehouseCapacity> Handle(GetWarehouseCurrentCapacityCommand request, CancellationToken cancellationToken)
        {
            Warehouse.Domain.Entities.Warehouse warehouse = await this.DatabaseContext.Warehouses.FindAsync(new object[] { request.WarehouseId }, cancellationToken);
            if (warehouse == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Warehouse_EntityNotFoundException"], request.WarehouseId));
            }

            return new WarehouseCapacity()
            {
                UsedPositions = this.DatabaseContext.Positions.Where(x => x.Section.WarehouseId == warehouse.Id && x.CountWare() > 0).LongCount(),
                FreePositions = this.DatabaseContext.Positions.Where(x => x.Section.WarehouseId == warehouse.Id && x.CountWare() == 0).LongCount(),
                TotalPositions = this.DatabaseContext.Positions.Where(x => x.Section.WarehouseId == warehouse.Id).LongCount()
            };
        }
    }
}
