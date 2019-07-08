using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Models
{
    public class WareAvailabilityInSection
    {
        public Ware Ware { get; set; }
        public Section Section { get; set; }
        public int UnitsAvailable { get; set; }
    }
}
