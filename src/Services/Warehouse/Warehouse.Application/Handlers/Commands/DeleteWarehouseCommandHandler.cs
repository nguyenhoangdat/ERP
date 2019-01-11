using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, Domain.Entities.Warehouse>
    {
        protected const string UpdateWarehouseCommandHandlerEntityNotFoundException = "Unable to delete Warehouse with id={0}. Warehouse not found!";

        public DeleteWarehouseCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Domain.Entities.Warehouse> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            if (this.DatabaseContext.Warehouses.Any(x => x.Id == request.Model.Id))
            {
                throw new EntityNotFoundException(string.Format(UpdateWarehouseCommandHandlerEntityNotFoundException, request.Model.Id));
            }

            Domain.Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.Find(request.Model.Id);
            warehouse = this.DatabaseContext.Warehouses.Remove(warehouse).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return warehouse;
        }
    }
}
