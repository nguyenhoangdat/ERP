using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class StockTakingItemUpdatedDomainEvent : INotification
    {
        public StockTakingItemUpdatedDomainEvent(StockTaking.Item item)
        {
            this.Item = item;
        }

        public StockTaking.Item Item { get; }
    }
}
