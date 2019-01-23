using MediatR;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class WarehouseCreatedDomainEvent : INotification
    {
        public WarehouseCreatedDomainEvent(Entities.Warehouse warehouse)
        {
            this.Warehouse = warehouse;
        }

        public Entities.Warehouse Warehouse { get; }
    }
}
