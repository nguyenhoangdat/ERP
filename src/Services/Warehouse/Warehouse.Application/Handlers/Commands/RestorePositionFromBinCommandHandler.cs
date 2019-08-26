using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RestorePositionFromBinCommandHandler : IRequestHandler<RestorePositionFromBinCommand, Position>
    {
        public RestorePositionFromBinCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Position> Handle(RestorePositionFromBinCommand request, CancellationToken cancellationToken)
        {
            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == request.PositionId);

            if (position == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Position_EntityNotFoundException, request.PositionId));
            }

            position.UtcMovedToBin = null;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return position;
        }
    }
}
