using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindStockTakingItemsOnPageCommand : IRequest<PageDto<StockTaking.Item>>
    {
        public FindStockTakingItemsOnPageCommand(int page, int itemsPerPage)
        {
            this.Page = page;
            this.ItemsPerPage = itemsPerPage;
        }

        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
