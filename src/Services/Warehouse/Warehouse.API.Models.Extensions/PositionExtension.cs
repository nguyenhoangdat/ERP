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

            int? count = position.Movements.OrderByDescending(x => x.DateCreated).FirstOrDefault()?.CountTotal;

            return count ?? 0;
        }
    }
}
