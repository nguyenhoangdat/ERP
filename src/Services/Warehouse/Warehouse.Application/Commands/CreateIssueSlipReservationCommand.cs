using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateIssueSlipReservationCommand : IRequest<Position>
    {
        public CreateIssueSlipReservationCommand(long positionId, int reservedUnits) : this(new CreateIssueSlipReservationCommandModel(positionId, reservedUnits))
        {

        }
        public CreateIssueSlipReservationCommand(CreateIssueSlipReservationCommandModel model)
        {
            this.Model = model;
        }

        public CreateIssueSlipReservationCommandModel Model { get; }

        public class CreateIssueSlipReservationCommandModel
        {
            public CreateIssueSlipReservationCommandModel(long positionId, int reservedUnits)
            {
                this.PositionId = positionId;
                this.ReservedUnits = reservedUnits;
            }

            public long PositionId { get; }
            public int ReservedUnits { get; }
        }
    }
}
