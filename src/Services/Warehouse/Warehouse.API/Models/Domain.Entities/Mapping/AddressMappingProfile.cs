using AutoMapper;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities.Mapping
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            this.CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
