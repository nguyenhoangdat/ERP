using AutoMapper;

namespace Restmium.ERP.Services.Warehouse.Application.Models.Mapping
{
    public class MovementMappingProfile : Profile
    {
        public MovementMappingProfile()
        {
            this.CreateMap<Domain.Entities.Movement, MovementDTO>().ReverseMap();
        }
    }
}
