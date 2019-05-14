using AutoMapper;

namespace Restmium.ERP.Services.Warehouse.Application.Models.Mapping
{
    public class ReceiptMappingProfile : Profile
    {
        public ReceiptMappingProfile()
        {
            this.CreateMap<Domain.Entities.Receipt, ReceiptDTO>().ReverseMap();
        }
    }
}
