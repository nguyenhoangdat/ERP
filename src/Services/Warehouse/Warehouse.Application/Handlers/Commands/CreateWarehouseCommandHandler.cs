using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, Warehouse.Domain.Entities.Warehouse>
    {
        public CreateWarehouseCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Warehouse.Domain.Entities.Warehouse> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            Warehouse.Domain.Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.Add(new Warehouse.Domain.Entities.Warehouse(request.Model.Name, request.Model.Address)).Entity;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new WarehouseCreatedDomainEvent(warehouse));

            return warehouse;
        }
    }
}
