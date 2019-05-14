using AutoMapper;

namespace Restmium.ERP.Services.Warehouse.Application.Models.Mapping
{
    public class StockTakingMappingProfile : Profile
    {
        public StockTakingMappingProfile()
        {
            this.CreateMap<Domain.Entities.StockTaking, StockTakingDTO>().ReverseMap();
        }
    }
}
