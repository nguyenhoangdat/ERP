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
    public class FindSectionsOnPageCommandHandler : IRequestHandler<FindSectionsOnPageCommand, PageDto<Section>>
    {
        protected DatabaseContext DatabaseContext { get; }

        public FindSectionsOnPageCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public async Task<PageDto<Section>> Handle(FindSectionsOnPageCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Section> sections = this.DatabaseContext.Sections.Where(x => x.UtcMovedToBin == null);

            return new PageDto<Section>(
                request.Page,
                request.ItemsPerPage,
                sections.Count(),
                sections.Skip(request.ItemsPerPage * --request.Page).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
