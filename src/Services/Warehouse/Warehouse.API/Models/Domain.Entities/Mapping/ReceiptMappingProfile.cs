using AutoMapper;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities.Mapping
{
    public class ReceiptMappingProfile : Profile
    {
        public ReceiptMappingProfile()
        {
            this.CreateMap<Receipt, ReceiptDTO>().ReverseMap();
        }
    }
}
