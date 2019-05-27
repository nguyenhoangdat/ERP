using AutoMapper;
using Restmium.ERP.Services.Warehouse.Application.Models;

namespace Restmium.ERP.Services.Warehouse.API.Models.Application.Mapping
{
    public class WareAvailabilityMappingProfile : Profile
    {
        public WareAvailabilityMappingProfile()
        {
            this.CreateMap<WareAvailability, WareAvailabilityDTO>().ReverseMap();
        }
    }
}
