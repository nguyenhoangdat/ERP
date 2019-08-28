using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class WareExtensions
    {
        public static bool CanBeMovedToBin(this Ware ware)
        {
            if (ware.UtcMovedToBin != null)
            {
                return false;
            }

            foreach (StockTaking.Item item in ware.StockTakingItems.Where(x => x.UtcMovedToBin == null))
            {
                if (item.CanBeMovedToBin() == false)
                {
                    return false;
                }
            }
            foreach (Receipt.Item item in ware.ReceiptItems.Where(x => x.UtcMovedToBin == null))
            {
                if (item.CanBeMovedToBin() == false)
                {
                    return false;
                }
            }
            foreach (StockTaking.Item item in ware.StockTakingItems.Where(x => x.UtcMovedToBin == null))
            {
                if (item.CanBeMovedToBin() == false)
                {
                    return false;
                }
            }

            return true;
        }
        public static bool CanBeRestoredFromBin(this Ware ware)
        {
            return ware.UtcMovedToBin != null;
        }
    }
}
