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
    public class FindWarehousesOnPageCommandHandler : IRequestHandler<FindWarehousesOnPageCommand, PageDTO<Entities.Warehouse>>
    {
        protected DatabaseContext DatabaseContext { get; set; }

        public FindWarehousesOnPageCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public async Task<PageDTO<Entities.Warehouse>> Handle(FindWarehousesOnPageCommand request, CancellationToken cancellationToken)
        {
            return new PageDTO<Entities.Warehouse>(
                request.Page,
                request.ItemsPerPage,
                this.DatabaseContext.Warehouses.Skip(request.ItemsPerPage * --request.Page).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
