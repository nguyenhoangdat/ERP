﻿using System;
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
        public static int CountAvailableWare(this Position position) // TODO: Use this method insted of CountWare for some commands
        {
            return CountWare(position) - position.ReservedUnits;
        }

        public static bool HasLoadCapacity(this Position position, int unitsTotal) => HasLoadCapacity(position, position.GetWare(), unitsTotal);
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

            return position.MaxWeight < ware.Weight * unitsTotal;
        }

        public static int MaxLoadCapacity(this Position position, Ware ware)
        {
            return Convert.ToInt32(Math.Floor(position.MaxWeight / ware.Weight));
        }

        public static bool HasSpaceCapacity(this Position position, int unitsTotal) => HasSpaceCapacity(position, position.GetWare(), unitsTotal);

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

            return true; //TODO: Implement HasSpaceCapacity
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
