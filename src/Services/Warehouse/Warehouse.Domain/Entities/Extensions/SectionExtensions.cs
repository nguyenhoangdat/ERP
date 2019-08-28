using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class SectionExtensions
    {
        public static bool CanBeMovedToBin(this Section section)
        {
            if (section.UtcMovedToBin != null)
            {
                return false;
            }

            foreach (Position item in section.Positions.Where(x => x.UtcMovedToBin == null))
            {
                if (item.CanBeMovedToBin() == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CanBeDeleted(this Section section)
        {
            throw new System.NotImplementedException("SectionExtensions.CanBeDeleted()"); // TODO: 2019.2 Implement CanBeDeleted
        }
    }
}
