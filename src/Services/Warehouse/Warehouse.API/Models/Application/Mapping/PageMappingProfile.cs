using AutoMapper;
using Restmium.ERP.Services.Warehouse.Application.Models;

namespace Restmium.ERP.Services.Warehouse.API.Models.Application.Mapping
{
    public class PageMappingProfile : Profile
    {
        public PageMappingProfile()
        {
            this.CreateMap(typeof(Page<>), typeof(PageDTO<>)).ReverseMap();
        }
    }
}
