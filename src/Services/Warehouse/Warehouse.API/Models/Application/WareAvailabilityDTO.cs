using Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Application
{
    public class WareAvailabilityDTO
    {
        public WareDTO Ware { get; set; }
        public WarehouseDTO Warehouse { get; set; }
        public int UnitsAvailable { get; set; }
    }
}
