using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
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
        public DeletePositionCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Position> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {
            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == request.Id);

            if (position == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Position_Delete_EntityNotFoundException"], request.Id));
            }

            if (position.CountWare() > 0)
            {
                throw new EntityDeleteException(string.Format(Resources.Exceptions.Values["Position_EntityDeleteException"], position.Id));
            }

            position = this.DatabaseContext.Positions.Remove(position).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new PositionDeletedDomainEvent(position), cancellationToken);

            return position;
        }
    }
}
