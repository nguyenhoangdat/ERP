using MediatR;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class MovementCreatedDomainEvent : INotification
    {
        public MovementCreatedDomainEvent(long positionId, int wareId, int countTotal)
        {
            this.PositionId = positionId;
            this.WareId = wareId;
            this.CountTotal = countTotal;
        }

        public long PositionId { get; }
        public int WareId { get; }
        public int CountTotal { get; }
    }
}
