using System;
using System.Linq;

namespace Warehouse.API.Models.Extensions
{
    public static class PositionExtension
    {
        public static int Count(this Position position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }
            if (!position.WareId.HasValue)
            {
                return 0;
            }

            int? count = position.Movements.OrderByDescending(x => x.UtcCreatedAt).FirstOrDefault()?.CountTotal;

            return count ?? 0;
        }
    }
}
