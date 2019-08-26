using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities = Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, Warehouse.Domain.Entities.Warehouse>
    {
        public UpdateWarehouseCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Entities.Warehouse> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.FirstOrDefault(x => x.Id == request.Id);

            if (warehouse == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Warehouse_Update_EntityNotFoundException, request.Id));
            }

            warehouse.Name = request.Name;
            warehouse.Address = request.Address;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            // Publish DomainEvent that the Warehouse has been updated
            await this.Mediator.Publish(new WarehouseUpdatedDomainEvent(warehouse), cancellationToken);

            return warehouse;
        }
    }
}
