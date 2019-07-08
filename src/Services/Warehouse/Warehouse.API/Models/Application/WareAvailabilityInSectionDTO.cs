using Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Application
{
    public class WareAvailabilityInSectionDTO
    {
        public WareDTO Ware { get; set; }
        public SectionDTO Section { get; set; }
        public int UnitsAvailable { get; set; }
    }
}
