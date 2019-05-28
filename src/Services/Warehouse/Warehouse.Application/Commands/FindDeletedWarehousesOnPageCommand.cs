using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindDeletedWarehousesOnPageCommand : IRequest<Page<Domain.Entities.Warehouse>>
    {
        public FindDeletedWarehousesOnPageCommand(int page, int itemsPerPage)
        {
            this.Page = page;
            this.ItemsPerPage = itemsPerPage;
        }

        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
