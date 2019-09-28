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
    public class GetWareAvailabilityInWarehousesCommandHandler : IRequestHandler<GetWareAvailabilityInWarehousesCommand, IEnumerable<WareAvailabilityInWarehouse>>
    {
        public GetWareAvailabilityInWarehousesCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IEnumerable<WareAvailabilityInWarehouse>> Handle(GetWareAvailabilityInWarehousesCommand request, CancellationToken cancellationToken)
        {
            Ware ware = this.DatabaseContext.Wares.FirstOrDefault(x => x.Id == request.WareId);
            if (ware == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Ware_EntityNotFoundException, request.WareId));
            }

            IQueryable<Position> positions = this.DatabaseContext.Positions.Where(x => 1 < x.Section.WarehouseId && x.GetWare().Id == request.WareId);

            return positions
                .GroupBy(e => e.Section.WarehouseId)
                .Select(eg => new WareAvailabilityInWarehouse()
                {
                    Warehouse = eg.First().Section.Warehouse,
                    Ware = eg.First().GetWare(),
                    UnitsAvailable = eg.Sum(e => e.CountAvailableWare())
                }).ToList();
        }
    }
}
