using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class MoveWarehouseToBinCommandHandler : IRequestHandler<MoveWarehouseToBinCommand, Warehouse.Domain.Entities.Warehouse>
    {
        public MoveWarehouseToBinCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Warehouse.Domain.Entities.Warehouse> Handle(MoveWarehouseToBinCommand request, CancellationToken cancellationToken)
        {
            Warehouse.Domain.Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.FirstOrDefault(x => x.Id == request.WarehouseId);

            if (warehouse == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Warehouse_EntityNotFoundException, request.WarehouseId));
            }

            warehouse.UtcMovedToBin = DateTime.UtcNow;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return warehouse;
        }
    }
}
