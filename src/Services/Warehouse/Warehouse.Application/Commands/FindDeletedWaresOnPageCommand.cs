using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindDeletedWaresOnPageCommand : IRequest<Page<Ware>>
    {
        public FindDeletedWaresOnPageCommand(int page, int itemsPerPage)
        {
            this.Page = page;
            this.ItemsPerPage = itemsPerPage;
        }

        public int Page { get; }
        public int ItemsPerPage { get; }
    }
}
