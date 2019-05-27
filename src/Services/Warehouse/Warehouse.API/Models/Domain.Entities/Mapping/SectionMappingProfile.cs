using AutoMapper;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities.Mapping
{
    public class SectionMappingProfile : Profile
    {
        public SectionMappingProfile()
        {
            this.CreateMap<Section, SectionDTO>().ReverseMap();
        }
    }
}
