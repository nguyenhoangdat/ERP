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
    public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Position>
    {
        protected const string UpdatePositionCommandHandlerEntityNotFoundException = "Unable to update Position with id={0}. Position not found!";

        public UpdatePositionCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Position> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Positions.Any(x => x.Id == request.Model.Id))
            {
                throw new EntityNotFoundException(string.Format(UpdatePositionCommandHandlerEntityNotFoundException, request.Model.Id));
            }

            Position position = this.DatabaseContext.Positions.Find(request.Model.Id);
            position.Name = request.Model.Name;
            position.Width = request.Model.Width;
            position.Height = request.Model.Height;
            position.Depth = request.Model.Depth;
            position.MaxWeight = request.Model.MaxWeight;
            position.Rating = request.Model.Rating;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return position;
        }
    }
}
