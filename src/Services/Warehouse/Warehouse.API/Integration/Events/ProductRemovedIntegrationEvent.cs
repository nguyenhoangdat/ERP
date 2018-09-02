using Restmium.ERP.BuildingBlocks.EventBus.Events;

namespace Warehouse.API.Integration.Events
{
    public class ProductRemovedIntegrationEvent : IntegrationEvent
    {
        public int ProductId { get; }
    }
}
