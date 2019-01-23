using MediatR;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class WarehouseDeletedDomainEvent : INotification
    {
        public WarehouseDeletedDomainEvent(Entities.Warehouse warehouse)
        {
            this.Warehouse = warehouse;
        }

        public Entities.Warehouse Warehouse { get; }
    }
}
