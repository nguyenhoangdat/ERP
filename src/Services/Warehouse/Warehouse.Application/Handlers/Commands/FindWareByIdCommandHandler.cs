using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindWareByIdCommandHandler : IRequestHandler<FindWareByIdCommand, Ware>
    {
        public FindWareByIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Ware> Handle(FindWareByIdCommand request, CancellationToken cancellationToken)
        {
            Ware ware = await this.DatabaseContext.Wares.FindAsync(new object[] { request.WareId }, cancellationToken);

            if (ware == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Ware_EntityNotFoundException"], request.WareId));
            }

            return ware;
        }
    }
}
