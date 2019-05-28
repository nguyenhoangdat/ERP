using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindDeletedStockTakingsOnPageCommand : IRequest<Page<StockTaking>>
    {
        public FindDeletedStockTakingsOnPageCommand(int page, int itemsPerPage)
        {
            this.Page = page;
            this.ItemsPerPage = itemsPerPage;
        }

        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
