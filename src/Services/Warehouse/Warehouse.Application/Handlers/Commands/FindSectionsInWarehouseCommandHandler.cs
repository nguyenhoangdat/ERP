using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities = Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindSectionsInWarehouseCommandHandler : IRequestHandler<FindSectionsInWarehouseCommand, IEnumerable<Section>>
    {
        public FindSectionsInWarehouseCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IEnumerable<Section>> Handle(FindSectionsInWarehouseCommand request, CancellationToken cancellationToken)
        {
            Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.FirstOrDefault(x => x.Id == request.WarehouseId);

            if (warehouse == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Warehouse_EntityNotFoundException, request.WarehouseId));
            }

            return warehouse.Sections;
        }
    }
}
