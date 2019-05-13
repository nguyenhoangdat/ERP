using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Models
{
    public class PageDTO<T>
    {
        public PageDTO(int pageId, int itemsPerPage, int itemsTotal, IEnumerable<T> items)
        {
            this.PageId = pageId;
            this.ItemsPerPage = itemsPerPage;
            this.ItemsTotal = itemsTotal;
            this.Items = items;
        }

        public int PageId { get; set; }
        public int ItemsPerPage { get; set; }
        public int ItemsTotal { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
