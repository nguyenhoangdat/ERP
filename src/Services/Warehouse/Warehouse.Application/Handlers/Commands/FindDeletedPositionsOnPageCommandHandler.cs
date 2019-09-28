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
    public class FindDeletedPositionsOnPageCommandHandler : IRequestHandler<FindDeletedPositionsOnPageCommand, Page<Position>>
    {
        public FindDeletedPositionsOnPageCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Page<Position>> Handle(FindDeletedPositionsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Position> positions = this.DatabaseContext.Positions.Where(x => 1 < x.Id && x.UtcMovedToBin != null);

            return new Page<Position>(
                request.Page,
                request.ItemsPerPage,
                positions.Count(),
                positions.Skip(request.ItemsPerPage * (request.Page - 1)).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
