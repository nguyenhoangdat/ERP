using Microsoft.EntityFrameworkCore;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database.Extensions
{
    public static class DbSetPositionExtension
    {
        /// <summary>
        /// Returns number of units of ware stored at position
        /// </summary>
        /// <param name="dbset">The instance of DbSet</param>
        /// <param name="position">The instance of Position</param>
        /// <returns></returns>
        public static int Count(this DbSet<Position> dbSet, Position position)
        {            
            return position.Movements.OrderByDescending(x => x.UtcCreated).FirstOrDefault()?.CountTotal ?? 0;
        }
        /// <summary>
        /// Returns the instance of Ware stored at position. Returns null if position is empty
        /// </summary>
        /// <param name="dbset">The instance of DbSet</param>
        /// <param name="position">The instance of Position</param>
        /// <returns></returns>
        public static Ware GetWare(this DbSet<Position> dbSet, Position position)
        {
            return position.Movements.OrderByDescending(x => x.UtcCreated).FirstOrDefault()?.Ware;
        }
        /// <summary>
        /// Returns a list of positions which contains specified Ware.
        /// </summary>
        /// <param name="dbSet"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IEnumerable<Position> GetWarePositions(this DbSet<Position> dbSet, Ware ware) => GetWarePositions(dbSet, ware.Id);
        /// <summary>
        /// Returns a list of positions where Ware.Id is equal to parameter id.
        /// </summary>
        /// <param name="dbSet"></param>
        /// <param name="wareId"></param>
        /// <returns></returns>
        public static IEnumerable<Position> GetWarePositions(this DbSet<Position> dbSet, int wareId)
        {
            return dbSet.Where(x => dbSet.GetWare(x) != null && dbSet.GetWare(x).Id == wareId);
        }
    }
}
