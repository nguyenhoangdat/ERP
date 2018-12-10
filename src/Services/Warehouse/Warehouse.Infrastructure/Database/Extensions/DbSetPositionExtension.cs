using Microsoft.EntityFrameworkCore;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database.Extensions
{
    public static class DbSetPositionExtension
    {
        /// <summary>
        /// Returns number of wares stored at position
        /// </summary>
        /// <param name="dbset">The instance of DbSet</param>
        /// <param name="position">The instance of Position</param>
        /// <returns></returns>
        public static int Count(this DbSet<Position> dbSet, Position position)
        {            
            return position.Movements.OrderByDescending(x => x.UtcCreated).FirstOrDefault()?.CountTotal ?? 0;
        }
        /// <summary>
        /// Return the instance of Ware stored at position. Returns null if position is empty
        /// </summary>
        /// <param name="dbset">The instance of DbSet</param>
        /// <param name="position">The instance of Position</param>
        /// <returns></returns>
        public static Ware GetWare(this DbSet<Position> dbSet, Position position)
        {
            return position.Movements.OrderByDescending(x => x.UtcCreated).FirstOrDefault()?.Ware;
        }
    }
}
