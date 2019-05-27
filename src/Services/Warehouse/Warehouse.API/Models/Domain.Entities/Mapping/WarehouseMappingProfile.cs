using AutoMapper;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities.Mapping
{
    public class WarehouseMappingProfile : Profile
    {
        public WarehouseMappingProfile()
        {
            this.CreateMap<Warehouse.Domain.Entities.Warehouse, WarehouseDTO>().ReverseMap();
        }
    }
}
