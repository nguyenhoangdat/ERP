using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, Position>
    {
        protected const string DeletePositionCommandHandlerEntityNotFoundException = "Unable to delete Position with Id={0}. Position not found!";

        public DeletePositionCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Position> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Positions.Any(x => x.Id == request.Model.Id))
            {
                throw new EntityNotFoundException(string.Format(DeletePositionCommandHandlerEntityNotFoundException, request.Model.Id));
            }

            Position position = this.DatabaseContext.Positions.Remove(this.DatabaseContext.Positions.Find(request.Model.Id)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new PositionDeletedDomainEvent(position));

            return position;
        }
    }
}
