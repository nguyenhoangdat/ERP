using AutoMapper;

namespace Restmium.ERP.Services.Warehouse.Application.Models.Mapping
{
    public class IssueSlipMappingProfile : Profile
    {
        public IssueSlipMappingProfile()
        {
            this.CreateMap<Domain.Entities.IssueSlip, IssueSlipDTO>().ReverseMap();
        }
    }
}
