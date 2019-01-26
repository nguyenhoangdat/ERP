using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class MovementDeletedDomainEvent : INotification
    {
        public MovementDeletedDomainEvent(Movement movement)
        {
            this.Movement = movement;
        }

        public Movement Movement { get; }
    }
}
