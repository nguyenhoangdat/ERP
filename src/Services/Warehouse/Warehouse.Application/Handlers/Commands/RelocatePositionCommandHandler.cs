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
        /// Relocates all Wares at one position to the second position and return Position to which were Wares relocated.
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
            Position positionFrom = this.DatabaseContext.Positions.Find(request.Model.FromPositionWithId);
            Position positionTo = this.DatabaseContext.Positions.Find(request.Model.ToPositionWithId);

            // Throw exceptions when Position is not found
            if (positionFrom == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Position_EntityNotFoundException"], request.Model.FromPositionWithId));
            }
            if (positionTo == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Position_EntityNotFoundException"], request.Model.ToPositionWithId));
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

            // Throw an exception if Position cannot hold the ammount of Wares that user want to relocate
            int countTotal = positionTo.CountWare() + positionFrom.CountWare();
            if (!positionTo.HasLoadCapacity(positionFrom.GetWare(), countTotal))
            {
                throw new PositionLoadCapacityException(string.Format(Resources.Exceptions.Values["Relocation_PositionLoadCapacityException"], positionFrom.Id, positionTo.Id));
            }
            if (!positionTo.HasSpaceCapacity(positionFrom.GetWare(), countTotal))
            {
                throw new NotImplementedException(); //TODO: Implement - HasSpaceCapacity
            }

            // Relocate Wares between Positions - create movements
            Ware ware = positionFrom.GetWare();
            int unitsToRelocate = positionFrom.CountWare();
            await this.Mediator.Send(new CreateMovementCommand(ware.Id, positionFrom.Id, Movement.Direction.Out, unitsToRelocate, 0), cancellationToken); //TODO: Add EmployeeId
            await this.Mediator.Send(new CreateMovementCommand(ware.Id, positionTo.Id, Movement.Direction.In, unitsToRelocate, 0), cancellationToken); //TODO: Add EmployeeId

            // Update ReservedUnits
            positionFrom = await this.Mediator.Send(new RemoveIssueSlipReservationCommand(positionFrom, unitsToRelocate), cancellationToken);
            positionTo = await this.Mediator.Send(new CreateIssueSlipReservationCommand(positionTo.Id, unitsToRelocate), cancellationToken);

            // Update IssueSlips through DomainEvent (PositionRelocatedDomainEvent)
            await this.Mediator.Publish(new PositionRelocatedDomainEvent(positionFrom, positionTo), cancellationToken);

            return positionTo;
        }
    }
}
