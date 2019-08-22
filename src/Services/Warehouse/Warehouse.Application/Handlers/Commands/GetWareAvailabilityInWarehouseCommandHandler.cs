using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class GetWareAvailabilityInWarehouseCommandHandler : IRequestHandler<GetWareAvailabilityInWarehouseCommand, WareAvailabilityInWarehouse>
    {
        public GetWareAvailabilityInWarehouseCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public DatabaseContext DatabaseContext { get; }

        public async Task<WareAvailabilityInWarehouse> Handle(GetWareAvailabilityInWarehouseCommand request, CancellationToken cancellationToken)
        {
            Ware ware = this.DatabaseContext.Wares.FirstOrDefault(x => x.Id == request.WareId);
            if (ware == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Ware_EntityNotFoundException"], request.WareId));
            }

            Warehouse.Domain.Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.FirstOrDefault(x => x.Id == request.WarehouseId);
            if (warehouse == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Warehouse_EntityNotFoundException"], request.WarehouseId));
            }

            IQueryable<Position> positions = this.DatabaseContext.Positions
                .Where(x =>
                    x.GetWare().Id == request.WareId &&
                    x.Section.WarehouseId == request.WarehouseId);

            WareAvailabilityInWarehouse wareAvailability = new WareAvailabilityInWarehouse()
            {
                Ware = ware,
                Warehouse = warehouse,
                UnitsAvailable = positions.Aggregate(0, (total, next) => total + next.CountAvailableWare())
            };

            return wareAvailability;
        }
    }
}
