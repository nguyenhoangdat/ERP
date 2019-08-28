using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
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
        public MoveWarehouseToBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Warehouse.Domain.Entities.Warehouse> Handle(MoveWarehouseToBinCommand request, CancellationToken cancellationToken)
        {
            Warehouse.Domain.Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.FirstOrDefault(x => x.Id == request.WarehouseId);

            if (warehouse == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Warehouse_EntityNotFoundException, request.WarehouseId));
            }
            if (request.MovedToBinInCascade == false)
            {
                if (warehouse.CanBeMovedToBin() == false)
                {
                    throw new EntityMoveToBinException(string.Format(Properties.Resources.Warehouse_EntityMoveToBinException, warehouse.Id));
                }
            }

            warehouse.UtcMovedToBin = DateTime.UtcNow;
            warehouse.MovedToBinInCascade = request.MovedToBinInCascade;

            foreach (Section item in warehouse.Sections.Where(x => x.UtcMovedToBin == null))
            {
                await this.Mediator.Send(new MoveSectionToBinCommand(item.Id, true), cancellationToken);
            }

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return warehouse;
        }
    }
}
