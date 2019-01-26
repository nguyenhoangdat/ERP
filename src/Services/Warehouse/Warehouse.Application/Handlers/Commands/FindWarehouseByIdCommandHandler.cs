using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindWarehouseByIdCommandHandler : IRequestHandler<FindWarehouseByIdCommand, Warehouse.Domain.Entities.Warehouse>
    {
        protected const string FindWarehouseByIdCommandHandler_EntityNotFoundException = "Entity Warehouse (Id={0}) not found!";

        public FindWarehouseByIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Warehouse.Domain.Entities.Warehouse> Handle(FindWarehouseByIdCommand request, CancellationToken cancellationToken)
        {
            Warehouse.Domain.Entities.Warehouse warehouse = await this.DatabaseContext.Warehouses.FindAsync(new object[] { request.Model.Id }, cancellationToken);

            if (warehouse == null)
            {
                throw new EntityNotFoundException(string.Format(FindWarehouseByIdCommandHandler_EntityNotFoundException, request.Model.Id));
            }

            return warehouse;
        }
    }
}
