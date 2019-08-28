using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class ReceiptExtensions
    {
        public static bool CanBeMovedToBin(this Receipt receipt)
        {
            return receipt.UtcMovedToBin != null ? false : !receipt.Items.Where(x => x.UtcMovedToBin == null).Any(x => x.UtcProcessed == null);
        }
    }
}
