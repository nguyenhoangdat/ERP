using Restmium.ERP.BuildingBlocks.EventBus.Events;

namespace Warehouse.API.Integration.Events
{
    public class WareAvailabilityChangedIntegrationEvent : IntegrationEvent
    {
        public WareAvailabilityChangedIntegrationEvent(int productId, int units) : base()
        {
            this.ProductId = productId;
            this.Units = units;
        }

        public int ProductId { get; set; }
        public int Units { get; set; }
    }
}
