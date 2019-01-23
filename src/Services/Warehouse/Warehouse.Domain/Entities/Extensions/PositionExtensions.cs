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
    }
}
