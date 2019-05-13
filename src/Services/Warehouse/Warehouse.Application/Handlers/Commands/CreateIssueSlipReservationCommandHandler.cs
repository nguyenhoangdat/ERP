using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateIssueSlipReservationCommandHandler : IRequestHandler<CreateIssueSlipReservationCommand, Position>
    {
        public CreateIssueSlipReservationCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Position> Handle(CreateIssueSlipReservationCommand request, CancellationToken cancellationToken)
        {
            Position position = this.DatabaseContext.Positions.Find(request.PositionId);
            position.ReservedUnits += request.ReservedUnits;

            position = this.DatabaseContext.Positions.Update(position).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new IssueSlipReservationCreatedDomainEvent(position), cancellationToken);

            return position;
        }
    }
}
