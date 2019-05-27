using AutoMapper;
using Restmium.ERP.Services.Warehouse.Application.Models;

namespace Restmium.ERP.Services.Warehouse.API.Models.Application.Mapping
{
    public class WarehouseCapacityProfileMapping : Profile
    {
        public WarehouseCapacityProfileMapping()
        {
            this.CreateMap<WarehouseCapacity, WarehouseCapacityDTO>().ReverseMap();
        }
    }
}
