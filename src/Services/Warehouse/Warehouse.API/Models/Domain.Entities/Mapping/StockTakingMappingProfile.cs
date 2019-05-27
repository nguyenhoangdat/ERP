using AutoMapper;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities.Mapping
{
    public class StockTakingMappingProfile : Profile
    {
        public StockTakingMappingProfile()
        {
            this.CreateMap<StockTaking, StockTakingDTO>().ReverseMap();
        }
    }
}
