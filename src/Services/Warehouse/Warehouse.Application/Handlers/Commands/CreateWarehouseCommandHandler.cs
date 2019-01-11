using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, Domain.Entities.Warehouse>
    {
        public CreateWarehouseCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Domain.Entities.Warehouse> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.Add(new Domain.Entities.Warehouse(request.Model.Name, request.Model.Address)).Entity;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return warehouse;
        }
    }
}
