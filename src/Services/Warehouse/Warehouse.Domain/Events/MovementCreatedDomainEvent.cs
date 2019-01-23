using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class MovementCreatedDomainEvent : INotification
    {
        public MovementCreatedDomainEvent(Movement movement)
        {
            this.Movement = movement;
        }

        public Movement Movement { get; }
    }
}
