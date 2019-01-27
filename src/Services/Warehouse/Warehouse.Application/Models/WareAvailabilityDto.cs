using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Models
{
    public class WareAvailabilityDTO
    {
        public Ware Ware { get; set; }
        public Domain.Entities.Warehouse Warehouse { get; set; }
        public int UnitsAvailable { get; set; }
    }
}
