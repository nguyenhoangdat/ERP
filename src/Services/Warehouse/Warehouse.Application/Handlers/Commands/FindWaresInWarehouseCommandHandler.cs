using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindWaresInWarehouseCommandHandler : IRequestHandler<FindWaresInWarehouseCommand, IEnumerable<Ware>>
    {
        public FindWaresInWarehouseCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IEnumerable<Ware>> Handle(FindWaresInWarehouseCommand request, CancellationToken cancellationToken)
        {
            return this.DatabaseContext.Positions
                .Where(x => x.Section.WarehouseId == request.Model.WarehouseId)
                .Select(x => x.GetWare())
                .AsEnumerable();
        }
    }
}
