using Restmium.Messaging;

namespace Restmium.ERP.Integration.Warehouse
{
    /// <summary>
    /// Integration event produced by Warehouse.API when availability of ware is changed
    /// </summary>
    public class WareAvailabilityChangedIntegrationEvent : IntegrationEvent
    {
        public WareAvailabilityChangedIntegrationEvent()
        {

        }
        public WareAvailabilityChangedIntegrationEvent(int productId, int units) : base()
        {
            this.ProductId = productId;
            this.Units = units;
        }

        public int ProductId { get; set; }
        public int Units { get; set; }
    }
}
