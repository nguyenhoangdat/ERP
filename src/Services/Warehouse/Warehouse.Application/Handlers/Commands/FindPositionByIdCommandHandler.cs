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
    public class FindPositionByIdCommandHandler : IRequestHandler<FindPositionByIdCommand, Position>
    {
        public FindPositionByIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public DatabaseContext DatabaseContext { get; }

        public async Task<Position> Handle(FindPositionByIdCommand request, CancellationToken cancellationToken)
        {
            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == request.Id);

            if (position == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Position_EntityNotFoundException, request.Id));
            }

            return position;
        }
    }
}
