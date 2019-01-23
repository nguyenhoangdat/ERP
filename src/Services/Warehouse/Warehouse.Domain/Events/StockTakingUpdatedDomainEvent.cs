using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class StockTakingUpdatedDomainEvent : INotification
    {
        public StockTakingUpdatedDomainEvent(StockTaking stockTaking)
        {
            this.StockTaking = stockTaking;
        }

        public StockTaking StockTaking { get; }
    }
}
