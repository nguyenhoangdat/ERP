using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class SectionExtensions
    {
        public static bool CanBeMovedToBin(this Section section, bool ignoreSystemEntity = false)
        {
            if ((ignoreSystemEntity == false && section.IsSystemEntity) || section.UtcMovedToBin != null)
            {
                return false;
            }

            foreach (Position item in section.Positions.Where(x => x.UtcMovedToBin == null))
            {
                if (item.CanBeMovedToBin(ignoreSystemEntity) == false)
                {
                    return false;
                }
            }

            return true;
        }
        public static bool CanBeRestoredFromBin(this Section section)
        {
            return section.UtcMovedToBin != null && section.Warehouse.CanBeMovedToBin();
        }

        public static bool CanBeDeleted(this Section section)
        {
            return section.UtcMovedToBin != null;
        }
    }
}
