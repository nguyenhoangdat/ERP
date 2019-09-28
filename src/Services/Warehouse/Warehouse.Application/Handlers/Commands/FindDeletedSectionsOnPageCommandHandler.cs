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
    public class FindDeletedSectionsOnPageCommandHandler : IRequestHandler<FindDeletedSectionsOnPageCommand, Page<Section>>
    {
        public FindDeletedSectionsOnPageCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Page<Section>> Handle(FindDeletedSectionsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Section> sections = this.DatabaseContext.Sections.Where(x => 1 < x.Id && x.UtcMovedToBin != null);

            return new Page<Section>(
                request.Page,
                request.ItemsPerPage,
                sections.Count(),
                sections.Skip(request.ItemsPerPage * (request.Page - 1)).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
