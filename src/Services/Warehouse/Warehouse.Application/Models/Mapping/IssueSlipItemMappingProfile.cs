using AutoMapper;

namespace Restmium.ERP.Services.Warehouse.Application.Models.Mapping
{
    public class IssueSlipItemMappingProfile : Profile
    {
        public IssueSlipItemMappingProfile()
        {
            this.CreateMap<Domain.Entities.IssueSlip.Item, IssueSlipDTO.ItemDTO>().ReverseMap();
        }
    }
}
