using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, Domain.Entities.Warehouse>
    {
        protected const string UpdateWarehouseCommandHandlerEntityNotFoundException = "Unable to update Warehouse with id={0}. Warehouse not found!";

        public UpdateWarehouseCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Domain.Entities.Warehouse> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Warehouses.Any(x => x.Id == request.Model.Id))
            {
                throw new EntityNotFoundException(string.Format(UpdateWarehouseCommandHandlerEntityNotFoundException, request.Model.Id));
            }

            Domain.Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.Find(request.Model.Id);
            warehouse.Name = request.Model.Name;
            warehouse.Address = request.Model.Address;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return warehouse;
        }
    }
}
