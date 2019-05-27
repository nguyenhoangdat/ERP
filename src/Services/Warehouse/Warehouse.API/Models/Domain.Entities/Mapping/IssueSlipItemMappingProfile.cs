using AutoMapper;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities.Mapping
{
    public class IssueSlipItemMappingProfile : Profile
    {
        public IssueSlipItemMappingProfile()
        {
            this.CreateMap<IssueSlip.Item, IssueSlipDTO.ItemDTO>().ReverseMap();
        }
    }
}
