using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class ReceiptExtensions
    {
        public static bool CanBeMovedToBin(this Receipt receipt)
        {
            return receipt.UtcMovedToBin != null ? false : !receipt.Items.Where(x => x.UtcMovedToBin == null).Any(x => x.CanBeMovedToBin() == false);
        }
        public static bool CanBeRestoredFromBin(this Receipt receipt)
        {
            return receipt.UtcMovedToBin != null;
        }
    }
}
