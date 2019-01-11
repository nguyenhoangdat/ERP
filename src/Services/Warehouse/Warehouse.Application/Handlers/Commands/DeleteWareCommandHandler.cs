using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class DeleteWareCommandHandler : IRequestHandler<DeleteWareCommand, Ware>
    {
        //TODO: Check grammar
        protected const string DeleteWareCommandHandlerEntityAlreadyExitsException = "Unable to delete ware with id={0}. Ware not found!";

        public DeleteWareCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Ware> Handle(DeleteWareCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Wares.Any(x => x.Id == request.Model.Id))
            {
                throw new EntityNotFoundException(string.Format(DeleteWareCommandHandlerEntityAlreadyExitsException, request.Model.Id));
            }

            Ware ware = this.DatabaseContext.Wares.Remove(this.DatabaseContext.Wares.Find(request.Model.Id)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return ware;
        }
    }
}
