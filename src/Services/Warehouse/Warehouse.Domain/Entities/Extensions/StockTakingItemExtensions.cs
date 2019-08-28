namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class StockTakingItemExtensions
    {
        public static bool CanBeMovedToBin(this StockTaking.Item item)
        {
            return item.UtcMovedToBin == null && item.UtcCounted != null;
        }
        public static bool CanBeRestoredFromBin(this StockTaking.Item item)
        {
            return
                item.UtcMovedToBin != null &&
                item.StockTaking.CanBeMovedToBin() &&
                item.Ware.CanBeMovedToBin() &&
                item.Position.CanBeMovedToBin();
        }
    }
}
