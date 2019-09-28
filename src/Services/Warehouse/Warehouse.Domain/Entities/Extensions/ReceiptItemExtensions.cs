using System;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class ReceiptItemExtensions
    {
        public static bool CanBeMovedToBin(this Receipt.Item item)
        {
            return
                item.UtcMovedToBin == null && ( // Not in a bin
                    item.UtcProcessed == null ||
                    (
                        item.UtcProcessed != null &&
                        item.CountOrdered == item.CountReceived &&
                        item.UtcDelete <= DateTime.UtcNow
                    ));
        }
        public static bool CanBeRestoredFromBin(this Receipt.Item item)
        {
            return
                item.UtcMovedToBin != null &&
                item.Receipt.UtcMovedToBin == null &&
                item.Ware.UtcMovedToBin == null &&
                item.Position.UtcMovedToBin == null;
        }
    }
}
