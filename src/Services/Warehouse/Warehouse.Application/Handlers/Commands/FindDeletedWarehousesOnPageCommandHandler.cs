using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities = Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindDeletedWarehousesOnPageCommandHandler : IRequestHandler<FindDeletedWarehousesOnPageCommand, Page<Entities.Warehouse>>
    {
        public FindDeletedWarehousesOnPageCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Page<Entities.Warehouse>> Handle(FindDeletedWarehousesOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Entities.Warehouse> warehouses = this.DatabaseContext.Warehouses.Where(x => 1 < x.Id && x.UtcMovedToBin != null);

            return new Page<Entities.Warehouse>(
                request.Page,
                request.ItemsPerPage,
                warehouses.Count(),
                warehouses.Skip(request.ItemsPerPage * (request.Page - 1)).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
