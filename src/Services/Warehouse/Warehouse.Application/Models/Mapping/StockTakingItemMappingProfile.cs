using AutoMapper;

namespace Restmium.ERP.Services.Warehouse.Application.Models.Mapping
{
    public class StockTakingItemMappingProfile : Profile
    {
        public StockTakingItemMappingProfile()
        {
            this.CreateMap<Domain.Entities.StockTaking.Item, StockTakingDTO.ItemDTO>().ReverseMap();
        }
    }
}
