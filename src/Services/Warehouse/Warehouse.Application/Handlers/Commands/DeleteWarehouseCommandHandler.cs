using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, Warehouse.Domain.Entities.Warehouse>
    {
        public DeleteWarehouseCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Warehouse.Domain.Entities.Warehouse> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            Warehouse.Domain.Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.FirstOrDefault(x => x.Id == request.Id);
            if (this.DatabaseContext.Warehouses.Any(x => x.Id == request.Id))
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Warehouse_Delete_EntityNotFoundException, request.Id));
            }

            warehouse = this.DatabaseContext.Warehouses.Remove(warehouse).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new WarehouseDeletedDomainEvent(warehouse), cancellationToken);

            return warehouse;
        }
    }
}
