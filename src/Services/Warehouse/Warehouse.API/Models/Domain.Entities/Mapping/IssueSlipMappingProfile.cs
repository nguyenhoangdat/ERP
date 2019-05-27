using AutoMapper;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities.Mapping
{
    public class IssueSlipMappingProfile : Profile
    {
        public IssueSlipMappingProfile()
        {
            this.CreateMap<IssueSlip, IssueSlipDTO>().ReverseMap();
        }
    }
}
