using System;
using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class PositionExtensions
    {
        private static Movement GetLastMovement(this Position position)
        {
            return position.Movements.OrderByDescending(x => x.UtcCreated).FirstOrDefault();
        }

        public static Ware GetWare(this Position position)
        {
            Movement movement = GetLastMovement(position);

            return movement == null || movement.CountTotal == 0 ? null : movement.Ware;
        }
        public static int CountWare(this Position position)
        {
            Movement movement = GetLastMovement(position);

            return movement == null ? 0 : movement.CountTotal;
        }
        public static int CountAvailableWare(this Position position)
        {
            return CountWare(position) - position.ReservedUnits;
        }

        public static bool HasCapacity(this Position position, Ware ware, int unitsTotal)
        {
            if (ware == null)
            {
                throw new ArgumentNullException(nameof(ware));
            }
            if (unitsTotal <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unitsTotal));
            }

            return unitsTotal <= MaxCapacity(position, ware);
        }
        public static bool HasLoadCapacity(this Position position, Ware ware, int unitsTotal)
        {
            if (ware == null)
            {
                throw new ArgumentNullException(nameof(ware));
            }
            if (unitsTotal <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unitsTotal));
            }

            return ware.Weight * unitsTotal <= position.MaxWeight;
        }
        public static bool HasSpaceCapacity(this Position position, Ware ware, int unitsTotal)
        {
            if (ware == null)
            {
                throw new ArgumentNullException(nameof(ware));
            }
            if (unitsTotal <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unitsTotal));
            }

            return unitsTotal <= MaxSpaceCapacity(position, ware);
        }

        public static int MaxCapacity(this Position position, Ware ware)
        {
            if (ware == null)
            {
                throw new ArgumentNullException(nameof(ware));
            }

            return Math.Min(MaxLoadCapacity(position, ware), MaxSpaceCapacity(position, ware));
        }
        public static int MaxLoadCapacity(this Position position, Ware ware)
        {
            if (ware == null)
            {
                throw new ArgumentNullException(nameof(ware));
            }

            return Convert.ToInt32(position.MaxWeight / ware.Weight);
        }
        public static int MaxSpaceCapacity(this Position position, Ware ware)
        {
            if (ware == null)
            {
                throw new ArgumentNullException(nameof(ware));
            }

            return MaxSpaceCapacity(position, ware, false); //HACK: Use property of Ware in 2020.1
        }

        private static int MaxSpaceCapacity(Position position, Ware ware, bool thisSideUp)
        {
            int max = 0;
            max = Math.Max(max, MaxSpaceCapacity(position, ware.Width, ware.Depth, ware.Height));
            max = Math.Max(max, MaxSpaceCapacity(position, ware.Depth, ware.Width, ware.Height));

            if (!thisSideUp)
            {
                max = Math.Max(max, MaxSpaceCapacity(position, ware.Height, ware.Depth, ware.Width));
                max = Math.Max(max, MaxSpaceCapacity(position, ware.Depth, ware.Height, ware.Width));

                max = Math.Max(max, MaxSpaceCapacity(position, ware.Height, ware.Width, ware.Depth));
                max = Math.Max(max, MaxSpaceCapacity(position, ware.Width, ware.Height, ware.Depth));
            }

            return max;
        }
        private static int MaxSpaceCapacity(Position position, double width, double depth, double height)
        {
            int maxWidth = Convert.ToInt32(position.Width / width);
            int maxDepth = Convert.ToInt32(position.Depth / depth);
            int maxHeight = Convert.ToInt32(position.Height / height);

            return maxWidth * maxDepth * maxHeight;
        }

        public static bool HasAllIssueSlipItemsProcessed(this Position position)
        {
            return position.IssueSlipItems.Count(x => x.IssuedUnits < x.RequestedUnits) == 0;
        }
        public static bool HasAllReceiptItemsProcessed(this Position position)
        {
            return position.ReceiptItems.Count(x => x.CountReceived < x.CountOrdered) == 0;
        }
        public static bool HasAllStockTakingItemsProcessed(this Position position)
        {
            return position.StockTakingItems.Count(x => x.UtcCounted == null) == 0;
        }
    }
}
