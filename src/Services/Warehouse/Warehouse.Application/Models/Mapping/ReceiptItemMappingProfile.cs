using AutoMapper;

namespace Restmium.ERP.Services.Warehouse.Application.Models.Mapping
{
    public class ReceiptItemMappingProfile : Profile
    {
        public ReceiptItemMappingProfile()
        {
            this.CreateMap<Domain.Entities.Receipt.Item, ReceiptDTO.ItemDTO>().ReverseMap();
        }
    }
}
