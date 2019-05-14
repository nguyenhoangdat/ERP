using AutoMapper;

namespace Restmium.ERP.Services.Warehouse.Application.Models.Mapping
{
    public class PositionMappingProfile : Profile
    {
        public PositionMappingProfile()
        {
            this.CreateMap<Domain.Entities.Position, PositionDTO>().ReverseMap();
        }
    }
}
