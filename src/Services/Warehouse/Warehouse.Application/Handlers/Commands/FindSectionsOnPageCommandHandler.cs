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
    public class FindSectionsOnPageCommandHandler : IRequestHandler<FindSectionsOnPageCommand, PageDTO<Section>>
    {
        protected DatabaseContext DatabaseContext { get; }

        public FindSectionsOnPageCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public async Task<PageDTO<Section>> Handle(FindSectionsOnPageCommand request, CancellationToken cancellationToken)
        {
            return new PageDTO<Section>(
                request.Page,
                request.ItemsPerPage,
                this.DatabaseContext.Sections.Skip(request.ItemsPerPage * --request.Page).Take(request.ItemsPerPage).AsEnumerable());
        }
    }
}
