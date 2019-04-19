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
    public class FindWarehousesOnPageCommandHandler : IRequestHandler<FindWarehousesOnPageCommand, PageDto<Entities.Warehouse>>
    {
        protected DatabaseContext DatabaseContext { get; set; }

        public FindWarehousesOnPageCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public async Task<PageDto<Entities.Warehouse>> Handle(FindWarehousesOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Entities.Warehouse> warehouses = this.DatabaseContext.Warehouses.Where(x => x.UtcMovedToBin == null);

            return new PageDto<Entities.Warehouse>(
                request.Page,
                request.ItemsPerPage,
                warehouses.Count(),
                warehouses.Skip(request.ItemsPerPage * --request.Page).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
