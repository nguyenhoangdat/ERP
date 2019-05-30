using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class MoveWareToBinCommandHandler : IRequestHandler<MoveWareToBinCommand, Ware>
    {
        public MoveWareToBinCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Ware> Handle(MoveWareToBinCommand request, CancellationToken cancellationToken)
        {
            Ware ware = await this.DatabaseContext.Wares.FindAsync(new object[] { request.WareId }, cancellationToken);

            if (ware == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Ware_EntityNotFoundException"], request.WareId));
            }

            ware.UtcMovedToBin = DateTime.UtcNow;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return ware;
        }
    }
}
