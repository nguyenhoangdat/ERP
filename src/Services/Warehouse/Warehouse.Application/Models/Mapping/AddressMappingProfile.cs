using AutoMapper;

namespace Restmium.ERP.Services.Warehouse.Application.Models.Mapping
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            this.CreateMap<Domain.Entities.Address, AddressDTO>().ReverseMap();
        }
    }
}
