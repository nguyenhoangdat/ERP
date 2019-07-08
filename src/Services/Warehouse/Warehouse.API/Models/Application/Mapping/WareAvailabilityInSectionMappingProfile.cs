using AutoMapper;
using Restmium.ERP.Services.Warehouse.Application.Models;

namespace Restmium.ERP.Services.Warehouse.API.Models.Application.Mapping
{
    public class WareAvailabilityInSectionMappingProfile : Profile
    {
        public WareAvailabilityInSectionMappingProfile()
        {
            this.CreateMap<WareAvailabilityInSection, WareAvailabilityInSectionDTO>();
        }
    }
}
