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
    public class MovePositionToBinCommandHandler : IRequestHandler<MovePositionToBinCommand, Position>
    {
        public MovePositionToBinCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Position> Handle(MovePositionToBinCommand request, CancellationToken cancellationToken)
        {
            Position position = await this.DatabaseContext.Positions.FindAsync(new object[] { request.PositionId }, cancellationToken);

            if (position == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Position_EntityNotFoundException"], request.PositionId));
            }

            position.UtcMovedToBin = DateTime.UtcNow;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return position;
        }
    }
}
