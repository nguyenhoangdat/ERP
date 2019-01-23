using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class PositionCreatedDomainEvent : INotification
    {
        public PositionCreatedDomainEvent(Position position)
        {
            this.Position = position;
        }

        public Position Position { get; }
    }
}
