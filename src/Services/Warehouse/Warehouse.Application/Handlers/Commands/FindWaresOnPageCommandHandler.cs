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
    public class FindWaresOnPageCommandHandler : IRequestHandler<FindWaresOnPageCommand, PageDto<Ware>>
    {
        protected DatabaseContext DatabaseContext { get; }

        public FindWaresOnPageCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        public async Task<PageDto<Ware>> Handle(FindWaresOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Ware> wares = this.DatabaseContext.Wares.Where(x => x.UtcMovedToBin == null);

            return new PageDto<Ware>(
                request.Page,
                request.ItemsPerPage,
                wares.Count(),
                wares.Skip(request.ItemsPerPage * --request.Page).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
