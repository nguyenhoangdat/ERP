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
    public class FindDeletedWaresOnPageCommandHandler : IRequestHandler<FindDeletedWaresOnPageCommand, Page<Ware>>
    {
        public FindDeletedWaresOnPageCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Page<Ware>> Handle(FindDeletedWaresOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Ware> wares = this.DatabaseContext.Wares.Where(x => x.UtcMovedToBin != null);

            return new Page<Ware>(
                request.Page,
                request.ItemsPerPage,
                wares.Count(),
                wares.Skip(request.ItemsPerPage * (request.Page - 1)).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
