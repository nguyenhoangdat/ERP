using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Entities = Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindSectionInWarehouseCommandHandler : IRequestHandler<FindSectionsInWarehouseCommand, IEnumerable<Section>>
    {
        public FindSectionInWarehouseCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IEnumerable<Section>> Handle(FindSectionsInWarehouseCommand request, CancellationToken cancellationToken)
        {
            Entities.Warehouse warehouse = await this.DatabaseContext.Warehouses.FindAsync(new object[] { request.Model.WarehouseId }, cancellationToken);

            if (warehouse == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Warehouse_EntityNotFoundException"], request.Model.WarehouseId));
            }

            return warehouse.Sections;
        }
    }
}
