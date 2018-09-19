using Restmium.ERP.BuildingBlocks.EventBus.Events;

namespace Ordering.API.Integration.Events
{
    public class OrderStartedIntegrationEvent : IntegrationEvent
    {
        public OrderStartedIntegrationEvent(long basketId) : base()
        {
            this.BasketId = basketId;
        }

        public long BasketId { get; set; }
    }
}
