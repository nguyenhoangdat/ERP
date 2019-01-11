using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Position>
    {
        public CreatePositionCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Position> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
        {
            Position position = this.DatabaseContext.Positions.Add(new Position(request.Model.Name, request.Model.Width, request.Model.Height, request.Model.Depth, request.Model.MaxWeight)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);
            return position;
        }
    }
}
