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
    public class UpdateWareCommandHandler : IRequestHandler<UpdateWareCommand, Ware>
    {
        //TODO: Check grammar
        protected const string UpdateWareCommandHandlerEntityAlreadyExitsException = "Unable to update ware with id={0}. Ware not found!";

        public UpdateWareCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Ware> Handle(UpdateWareCommand request, CancellationToken cancellationToken)
        {
            if (this.DatabaseContext.Wares.Any(x => x.Id == request.Ware.Id))
            {
                throw new EntityNotFoundException(string.Format(UpdateWareCommandHandlerEntityAlreadyExitsException, request.Ware.Id));
            }

            Ware ware = this.DatabaseContext.Wares.Find(request.Ware.Id);
            ware.ProductName = request.Ware.ProductName;
            ware.Width = request.Ware.Width;
            ware.Height = request.Ware.Height;
            ware.Depth = request.Ware.Depth;
            ware.Weight = request.Ware.Weight;

            await this.DatabaseContext.SaveChangesAsync();

            return ware;
        }
    }
}
