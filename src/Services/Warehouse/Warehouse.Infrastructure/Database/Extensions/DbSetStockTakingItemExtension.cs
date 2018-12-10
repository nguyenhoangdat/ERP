using Microsoft.EntityFrameworkCore;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database.Extensions
{
    public static class DbSetStockTakingItemExtension
    {
        /// <summary>
        /// Returns the difference between Counted and Expected number of units.
        /// </summary>
        /// <param name="context">The instance of DatabaseContext</param>
        /// <param name="item">The instance of StockTaking.Item</param>
        /// <returns></returns>
        public static int GetVariance(this DbSet<StockTaking.Item> dbSet, StockTaking.Item item)
        {
            return item.CountedStock - item.CurrentStock;
        }
    }
}
