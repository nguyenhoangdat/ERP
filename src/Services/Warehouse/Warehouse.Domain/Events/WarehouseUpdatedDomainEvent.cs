using MediatR;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class WarehouseUpdatedDomainEvent : INotification
    {
        public WarehouseUpdatedDomainEvent(Entities.Warehouse warehouse)
        {
            this.Warehouse = warehouse;
        }

        public Entities.Warehouse Warehouse { get; }
    }
}
