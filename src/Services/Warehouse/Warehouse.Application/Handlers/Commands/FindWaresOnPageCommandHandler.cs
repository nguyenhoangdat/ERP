using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindWaresOnPageCommandHandler : IRequestHandler<FindWaresOnPageCommand, PageDTO<Ware>>
    {
        protected DatabaseContext DatabaseContext { get; }

        public FindWaresOnPageCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        public async Task<PageDTO<Ware>> Handle(FindWaresOnPageCommand request, CancellationToken cancellationToken)
        {
            return new PageDTO<Ware>(
                request.Page,
                request.ItemsPerPage,
                this.DatabaseContext.Wares.Skip(request.ItemsPerPage * --request.Page).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
