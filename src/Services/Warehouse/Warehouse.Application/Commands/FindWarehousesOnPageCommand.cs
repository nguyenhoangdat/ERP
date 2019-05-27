using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindWarehousesOnPageCommand : IRequest<Page<Domain.Entities.Warehouse>>
    {
        public FindWarehousesOnPageCommand(int page, int itemsPerPage)
        {
            this.Page = page;
            this.ItemsPerPage = itemsPerPage;
        }

        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
