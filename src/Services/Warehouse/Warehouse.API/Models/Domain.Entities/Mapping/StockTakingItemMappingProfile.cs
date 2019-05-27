using AutoMapper;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities.Mapping
{
    public class StockTakingItemMappingProfile : Profile
    {
        public StockTakingItemMappingProfile()
        {
            this.CreateMap<StockTaking.Item, StockTakingDTO.ItemDTO>().ReverseMap();
        }
    }
}
