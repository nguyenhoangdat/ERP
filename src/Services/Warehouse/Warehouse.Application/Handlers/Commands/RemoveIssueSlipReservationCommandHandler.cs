using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RemoveIssueSlipReservationCommandHandler : IRequestHandler<RemoveIssueSlipReservationCommand, Position>
    {
        public RemoveIssueSlipReservationCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Position> Handle(RemoveIssueSlipReservationCommand request, CancellationToken cancellationToken)
        {
            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == request.PositionId);
            position.ReservedUnits -= request.ReservedUnitsToRemove;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            // Publish DomainEvent that the reservation has been removed
            await this.Mediator.Publish(new IssueSlipReservationRemovedDomainEvent(position), cancellationToken);

            return position;
        }
    }
}
