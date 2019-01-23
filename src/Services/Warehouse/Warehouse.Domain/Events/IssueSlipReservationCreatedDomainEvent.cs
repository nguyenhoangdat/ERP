using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class IssueSlipReservationCreatedDomainEvent : INotification
    {
        public IssueSlipReservationCreatedDomainEvent(Position position)
        {
            this.Position = position;
        }

        public Position Position { get; }
    }
}
