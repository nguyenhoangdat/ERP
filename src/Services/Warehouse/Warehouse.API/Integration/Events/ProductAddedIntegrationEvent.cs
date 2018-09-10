using Restmium.ERP.BuildingBlocks.EventBus.Events;

namespace Warehouse.API.Integration.Events
{
    public class ProductAddedIntegrationEvent : IntegrationEvent
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
