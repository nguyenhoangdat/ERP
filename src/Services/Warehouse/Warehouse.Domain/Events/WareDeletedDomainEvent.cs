using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class WareDeletedDomainEvent : INotification
    {
        public WareDeletedDomainEvent(Ware ware)
        {
            this.Ware = ware;
        }

        public Ware Ware { get; }
    }
}
