using AutoMapper;

namespace Restmium.ERP.Services.Warehouse.Application.Models.Mapping
{
    public class SectionMappingProfile : Profile
    {
        public SectionMappingProfile()
        {
            this.CreateMap<Domain.Entities.Section, SectionDTO>().ReverseMap();
        }
    }
}
