using System;
using System.Linq;

namespace Warehouse.API.Models.Extensions
{
    public static class StoredItemExtension
    {
        public static int Count(this StoredItem storedItem)
        {
            if (storedItem == null)
            {
                throw new ArgumentNullException(nameof(storedItem));
            }

            return storedItem.Positions.Sum(x => x.Count());
        }
    }
}
