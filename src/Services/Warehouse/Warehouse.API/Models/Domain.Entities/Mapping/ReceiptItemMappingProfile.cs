using AutoMapper;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities.Mapping
{
    public class ReceiptItemMappingProfile : Profile
    {
        public ReceiptItemMappingProfile()
        {
            this.CreateMap<Receipt.Item, ReceiptDTO.ItemDTO>().ReverseMap();
        }
    }
}
