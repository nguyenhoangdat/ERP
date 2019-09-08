namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class IssueSlipItemExtensions
    {
        public static bool CanBeMovedToBin(this IssueSlip.Item item)
        {
            return item.UtcMovedToBin == null && item.RequestedUnits == item.IssuedUnits;
        }
        public static bool CanBeRestoredFromBin(this IssueSlip.Item item)
        {
            return
                item.UtcMovedToBin != null &&
                item.IssueSlip.UtcMovedToBin == null &&
                item.Ware.UtcMovedToBin == null &&
                item.Position.UtcMovedToBin == null;
        }
    }
}
