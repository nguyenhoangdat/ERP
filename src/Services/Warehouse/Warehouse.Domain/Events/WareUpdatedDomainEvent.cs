using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class WareUpdatedDomainEvent : INotification
    {
        public WareUpdatedDomainEvent(Ware ware)
        {
            this.Ware = ware;
        }

        public Ware Ware { get; }
    }
}
