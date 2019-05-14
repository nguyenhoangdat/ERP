using AutoMapper;

namespace Restmium.ERP.Services.Warehouse.Application.Models.Mapping
{
    public class WareMappingProfile : Profile
    {
        public WareMappingProfile()
        {
            this.CreateMap<Domain.Entities.Ware, WareDTO>().ReverseMap();
        }
    }
}
