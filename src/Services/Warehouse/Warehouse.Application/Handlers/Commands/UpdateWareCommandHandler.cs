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
            if (this.DatabaseContext.Wares.Any(x => x.Id == request.Model.Id))
            {
                throw new EntityNotFoundException(string.Format(UpdateWareCommandHandlerEntityAlreadyExitsException, request.Model.Id));
            }

            Ware ware = this.DatabaseContext.Wares.Find(request.Model.Id);
            ware.ProductName = request.Model.ProductName;
            ware.Width = request.Model.Width;
            ware.Height = request.Model.Height;
            ware.Depth = request.Model.Depth;
            ware.Weight = request.Model.Weight;

            await this.DatabaseContext.SaveChangesAsync();

            return ware;
        }
    }
}
