using AutoMapper;
using Restmium.ERP.Services.Warehouse.Application.Models;

namespace Restmium.ERP.Services.Warehouse.API.Models.Application.Mapping
{
    public class WareAvailabilityInWarehouseMappingProfile : Profile
    {
        public WareAvailabilityInWarehouseMappingProfile()
        {
            this.CreateMap<WareAvailabilityInWarehouse, WareAvailabilityInWarehouseDTO>();
        }
    }
}
