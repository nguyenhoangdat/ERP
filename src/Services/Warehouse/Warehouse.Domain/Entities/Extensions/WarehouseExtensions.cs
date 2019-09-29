using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class WarehouseExtensions
    {
        public static bool CanBeMovedToBin(this Warehouse warehouse)
        {
            if (warehouse.IsSystemEntity || warehouse.UtcMovedToBin != null)
            {
                return false;
            }

            foreach (Section item in warehouse.Sections.Where(x => x.UtcMovedToBin == null))
            {
                if (item.CanBeMovedToBin(!warehouse.IsSystemEntity) == false)
                {
                    return false;
                }
            }

            return true;
        }
        public static bool CanBeRestoredFromBin(this Warehouse warehouse)
        {
            return warehouse.UtcMovedToBin != null;
        }
    }
}
