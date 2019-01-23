using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class PositionDeletedDomainEvent : INotification
    {
        public PositionDeletedDomainEvent(Position position)
        {
            this.Position = position;
        }

        public Position Position { get; }
    }
}
