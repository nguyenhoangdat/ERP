using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RestoreWarehouseFromBinCommandHandler : IRequestHandler<RestoreWarehouseFromBinCommand, Warehouse.Domain.Entities.Warehouse>
    {
        public RestoreWarehouseFromBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Warehouse.Domain.Entities.Warehouse> Handle(RestoreWarehouseFromBinCommand request, CancellationToken cancellationToken)
        {
            Warehouse.Domain.Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.FirstOrDefault(x => x.Id == request.WarehouseId);

            if (warehouse == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Warehouse_EntityNotFoundException, request.WarehouseId));
            }
            if (warehouse.CanBeRestoredFromBin() == false)
            {
                throw new EntityRestoreFromBinException(string.Format(Properties.Resources.Warehouse_EntityRestoreFromBinException, request.WarehouseId));
            }

            warehouse.UtcMovedToBin = null;
            warehouse.MovedToBinInCascade = false;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            foreach (Warehouse.Domain.Entities.Section item in warehouse.Sections.Where(x => x.UtcMovedToBin != null))
            {
                await this.Mediator.Send(new RestoreSectionFromBinCommand(item.Id), cancellationToken);
            }

            return warehouse;
        }
    }
}
