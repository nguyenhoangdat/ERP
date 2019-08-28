using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class StockTakingExtensions
    {
        public static bool CanBeMovedToBin(this StockTaking stockTaking)
        {
            return stockTaking.UtcMovedToBin != null ? false : !stockTaking.Items.Where(x => x.UtcMovedToBin == null).Any(x => x.UtcCounted == null);
        }
    }
}
