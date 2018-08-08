using System;

namespace Warehouse.API.Models.Extensions
{
    public static class StoredItemExtension
    {
        public static int? Count(this StoredItem storedItem)
        {
            if (storedItem == null)
            {
                throw new ArgumentNullException(nameof(storedItem));
            }

            int count = 0;

            foreach (Position item in storedItem.Positions)
            {
                int? itemCount = item.Count();

                if (itemCount.HasValue)
                    count += itemCount.Value;
            }

            return count;
        }
    }
}
