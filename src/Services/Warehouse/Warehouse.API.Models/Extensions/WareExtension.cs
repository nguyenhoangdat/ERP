using System;
using System.Linq;

namespace Warehouse.API.Models.Extensions
{
    public static class WareExtension
    {
        public static int Count(this Ware ware)
        {
            if (ware == null)
            {
                throw new ArgumentNullException(nameof(ware));
            }

            return ware.Positions.Sum(x => x.Count());
        }
    }
}
