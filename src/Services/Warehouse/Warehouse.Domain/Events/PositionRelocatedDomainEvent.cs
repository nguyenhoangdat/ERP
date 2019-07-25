using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class PositionRelocatedDomainEvent : INotification
    {
        public PositionRelocatedDomainEvent(Position positionFrom, Position positionTo, int relocatedUnits)
        {
            this.PositionFrom = positionFrom;
            this.PositionTo = positionTo;
            this.RelocatedUnits = relocatedUnits;
        }

        public Position PositionFrom { get; }
        public Position PositionTo { get; }
        public int RelocatedUnits { get; }
    }
}
