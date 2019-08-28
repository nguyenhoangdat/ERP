namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class MovementExtensions
    {
        public static bool CanBeMovedToBin(this Movement movement)
        {
            return movement.UtcMovedToBin == null;
        }

        public static bool CanBeRestoredFromBin(this Movement movement)
        {
            return
                movement.UtcMovedToBin != null &&
                movement.Ware.CanBeMovedToBin() &&
                movement.Position.CanBeMovedToBin();
        }
    }
}
