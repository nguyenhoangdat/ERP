using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;
using Entities = Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, Entities.Warehouse>
    {
        public CreateWarehouseCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Entities.Warehouse> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            // Create Warehouse and Save it in the Database
            Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.Add(new Entities.Warehouse(request.Model.Name, request.Model.Address, request.Model.Sections)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            // Publish DomainEvent that the Warehouse has been created
            await this.Mediator.Publish(new WarehouseCreatedDomainEvent(warehouse));

            return warehouse;
        }
    }
}
