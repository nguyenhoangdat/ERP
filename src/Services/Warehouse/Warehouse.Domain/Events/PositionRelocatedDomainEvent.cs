using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class PositionRelocatedDomainEvent : INotification
    {
        public PositionRelocatedDomainEvent(Position positionFrom, Position positionTo)
        {
            this.PositionFrom = positionFrom;
            this.PositionTo = positionTo;
        }

        public Position PositionFrom { get; }
        public Position PositionTo { get; }
    }
}
