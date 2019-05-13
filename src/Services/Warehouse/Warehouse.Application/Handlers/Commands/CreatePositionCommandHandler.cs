using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, Position>
    {
        public CreatePositionCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Position> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            Position position = this.DatabaseContext.Positions.Add(new Position(request.Name, request.Width, request.Height, request.Depth, request.MaxWeight)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new PositionCreatedDomainEvent(position), cancellationToken);

            return position;
        }
    }
}
