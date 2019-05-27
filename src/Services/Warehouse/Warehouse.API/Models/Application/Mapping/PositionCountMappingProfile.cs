using AutoMapper;
using Restmium.ERP.Services.Warehouse.Application.Models;

namespace Restmium.ERP.Services.Warehouse.API.Models.Application.Mapping
{
    public class PositionCountMappingProfile : Profile
    {
        public PositionCountMappingProfile()
        {
            this.CreateMap<PositionCount, PositionCountDTO>().ReverseMap();
        }
    }
}
