using System;
using System.Linq;

namespace Warehouse.API.Models.Extensions
{
    public static class PositionExtension
    {
        public static int? Count(this Position position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }

            return position.Movements.OrderByDescending(x => x.MovementDate).FirstOrDefault()?.CountTotal;
        }

        public static StoredItem StoredItem(this Position position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }

            return position.Movements.OrderByDescending(x => x.MovementDate).FirstOrDefault()?.StoredItem;
        }

        public static int? StoredItemId(this Position position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }

            return position.Movements.OrderByDescending(x => x.MovementDate).FirstOrDefault()?.StoredItemId;
        }
    }
}
