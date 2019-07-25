using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RelocatePositionCommandHandler : IRequestHandler<RelocatePositionCommand, Position>
    {
        public RelocatePositionCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        /// <summary>
        /// Relocates all Wares at one position to the second position and return <see cref="Position"/> to which were Wares relocated.
        /// 
        /// Throws an exception when:
        ///     One, or both positions are null
        ///     Position from which should be Wares transfered is empty
        ///     Positions store different Wares
        ///     Position to which we want to relocate does not have Load, or Space capacity to hold Wares at Position from which they should be transfered
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Position to which were the Wares transfered</returns>
        public async Task<Position> Handle(RelocatePositionCommand request, CancellationToken cancellationToken)
        {
            Position positionFrom = await this.DatabaseContext.Positions.FindAsync(new object[] { request.FromPositionWithId }, cancellationToken);
            Position positionTo = await this.DatabaseContext.Positions.FindAsync(new object[] { request.ToPositionWithId }, cancellationToken);

            // Throw exceptions when Position is not found
            if (positionFrom == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Position_EntityNotFoundException"], request.FromPositionWithId));
            }
            if (positionTo == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Position_EntityNotFoundException"], request.ToPositionWithId));
            }

            // Throw an exception when Position from which is transfered is empty
            if (positionFrom.CountWare() == 0)
            {
                throw new PositionEmptyException(string.Format(Resources.Exceptions.Values["Relocation_PositionEmptyException"], positionFrom.Id));
            }

            // Throw an exception when Positions do not store that same Ware
            if (positionFrom.GetWare() != positionTo.GetWare() && positionTo.GetWare() != null)
            {
                throw new PositionWareConflictException(string.Format(Resources.Exceptions.Values["Relocation_PositionWareConflictException"], positionFrom.Id, positionTo.Id));
            }

            // Relocate Wares between Positions - create movements
            Ware ware = positionFrom.GetWare();
            int unitsToRelocate = positionFrom.CountWare();
            await this.Mediator.Send(new CreateMovementCommand(ware.Id, positionTo.Id, Movement.Direction.In, unitsToRelocate), cancellationToken);
            await this.Mediator.Send(new CreateMovementCommand(ware.Id, positionFrom.Id, Movement.Direction.Out, unitsToRelocate), cancellationToken);

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            // Update IssueSlips through DomainEvent (PositionRelocatedDomainEvent)
            await this.Mediator.Publish(new PositionRelocatedDomainEvent(positionFrom, positionTo, unitsToRelocate), cancellationToken);

            return positionTo;
        }
    }
}
