using AutoMapper;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities.Mapping
{
    public class MovementMappingProfile : Profile
    {
        public MovementMappingProfile()
        {
            this.CreateMap<Movement, MovementDTO>().ReverseMap();
        }
    }
}
