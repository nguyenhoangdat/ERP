using System;
using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class MovementExtensions
    {
        public static bool CanBeMovedToBin(this Movement movement)
        {
            return
                movement.UtcMovedToBin == null && // Not in a bin
                movement.UtcDelete <= DateTime.UtcNow && // Retention period has passed

                // Problem with testing is here (won't appear in deployment) - possible to solve by using IDs
                movement.Position.Movements.Any(x => x.UtcCreated > movement.UtcCreated); // Newer movement exists
        }

        public static bool CanBeRestoredFromBin(this Movement movement)
        {
            return
                movement.UtcMovedToBin != null &&
                movement.Ware.UtcMovedToBin == null &&
                movement.Position.UtcMovedToBin == null;
        }
    }
}
