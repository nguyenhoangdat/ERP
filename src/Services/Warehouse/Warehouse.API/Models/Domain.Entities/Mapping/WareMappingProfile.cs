using AutoMapper;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities.Mapping
{
    public class WareMappingProfile : Profile
    {
        public WareMappingProfile()
        {
            this.CreateMap<Ware, WareDTO>().ReverseMap();
        }
    }
}
