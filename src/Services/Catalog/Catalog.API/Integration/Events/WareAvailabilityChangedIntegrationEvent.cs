using Restmium.ERP.BuildingBlocks.EventBus.Events;

namespace Catalog.API.Integration.Events
{
    public class WareAvailabilityChangedIntegrationEvent : IntegrationEvent
    {
        public int ProductId { get; set; }
        public int Units { get; set; }
    }
}
