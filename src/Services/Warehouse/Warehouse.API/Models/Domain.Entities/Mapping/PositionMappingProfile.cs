using AutoMapper;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities.Mapping
{
    public class PositionMappingProfile : Profile
    {
        public PositionMappingProfile()
        {
            this.CreateMap<Position, PositionDTO>().ReverseMap();
        }
    }
}
