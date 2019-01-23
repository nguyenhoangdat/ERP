using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class StockTakingCreatedDomainEvent : INotification
    {
        public StockTakingCreatedDomainEvent(StockTaking stockTaking)
        {
            this.StockTaking = stockTaking;
        }

        public StockTaking StockTaking { get; }
    }
}
