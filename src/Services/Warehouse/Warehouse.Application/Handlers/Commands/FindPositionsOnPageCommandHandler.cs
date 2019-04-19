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
    public class FindPositionsOnPageCommandHandler : IRequestHandler<FindPositionsOnPageCommand, PageDto<Position>>
    {
        protected DatabaseContext DatabaseContext { get; set; }

        public FindPositionsOnPageCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public async Task<PageDto<Position>> Handle(FindPositionsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Position> positions = this.DatabaseContext.Positions.Where(x => x.UtcMovedToBin == null);

            return new PageDto<Position>(
                request.Page,
                request.ItemsPerPage,
                positions.Count(),
                positions.Skip(request.ItemsPerPage * --request.Page).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
