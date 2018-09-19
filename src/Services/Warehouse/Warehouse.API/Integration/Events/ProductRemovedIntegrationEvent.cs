using Restmium.ERP.BuildingBlocks.EventBus.Events;

namespace Warehouse.API.Integration.Events
{
    /// <summary>
    /// Event produced by Catalog.API
    /// </summary>
    public class ProductRemovedIntegrationEvent : IntegrationEvent
    {
        public int ProductId { get; set; }
    }
}
