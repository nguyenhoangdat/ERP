using AutoMapper;

namespace Restmium.ERP.Services.Warehouse.Application.Models.Mapping
{
    public class WarehouseMappingProfile : Profile
    {
        public WarehouseMappingProfile()
        {
            this.CreateMap<Domain.Entities.Warehouse, WarehouseDTO>().ReverseMap();
        }
    }
}
