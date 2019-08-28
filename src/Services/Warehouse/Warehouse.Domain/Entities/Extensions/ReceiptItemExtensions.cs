namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class ReceiptItemExtensions
    {
        public static bool CanBeMovedToBin(this Receipt.Item item)
        {
            return item.UtcMovedToBin == null && item.UtcProcessed != null;
        }
        public static bool CanBeRestoredFromBin(this Receipt.Item item)
        {
            return
                item.UtcMovedToBin != null &&
                item.Receipt.CanBeMovedToBin() &&
                item.Ware.CanBeMovedToBin() &&
                item.Position.CanBeMovedToBin();
        }
    }
}
