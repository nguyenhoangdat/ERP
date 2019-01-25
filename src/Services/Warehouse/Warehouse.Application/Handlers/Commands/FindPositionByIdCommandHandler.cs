using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindPositionByIdCommandHandler : IRequestHandler<FindPositionByIdCommand, Position>
    {
        protected const string FindPositionByIdCommandHandler_EntityNotFoundException = "Position(Id={0}) not found!";

        public FindPositionByIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public DatabaseContext DatabaseContext { get; }

        public async Task<Position> Handle(FindPositionByIdCommand request, CancellationToken cancellationToken)
        {
            Position position = await this.DatabaseContext.Positions.FindAsync(request.Model.Id, cancellationToken);

            if (position == null)
            {
                throw new EntityNotFoundException(string.Format(FindPositionByIdCommandHandler_EntityNotFoundException, request.Model.Id));
            }

            return position;
        }
    }
}
