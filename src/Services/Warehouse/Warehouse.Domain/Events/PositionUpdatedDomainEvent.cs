using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class PositionUpdatedDomainEvent : INotification
    {
        public PositionUpdatedDomainEvent(Position position)
        {
            this.Position = position;
        }

        public Position Position { get; }
    }
}
