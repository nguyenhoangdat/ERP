using Microsoft.EntityFrameworkCore;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database.Extensions
{
    public static class DbSetWareExtension
    {
        /// <summary>
        /// Returns the number of units of ware stored across all warehouses
        /// </summary>
        /// <param name="context">The instance of DatabaseContext</param>
        /// <param name="ware">The instance of Ware</param>
        /// <returns></returns>
        public static int Count(this DbSet<Ware> dbSet, Ware ware)
        {
            // Get ids of latest movements at positions
            ICollection<long> ids =
                (
                    from movement in ware.Movements
                    orderby movement.PositionId, movement.UtcCreated
                    group movement by movement.PositionId into list
                    let id = list.Max(x => x.Id)
                    select id
                ).ToList(); //https://www.dotnetcurry.com/ShowArticle.aspx?ID=414

            // Return SUM of CountTotal of movements where id is contained in list ids
            return ware.Movements.Where(x => ids.Contains(x.Id) && x.CountTotal > 0).Sum(x => x.CountTotal);
        }
        /// <summary>
        /// Return the number of units of ware stored in specified warehouse
        /// </summary>
        /// <param name="context">The instance of DatabaseContext</param>
        /// <param name="ware">The instance of Ware</param>
        /// <param name="warehouse">The instance of Warehouse in which we want to count the units of Ware</param>
        /// <returns></returns>
        public static int Count(this DbSet<Ware> dbSet, Ware ware, Domain.Entities.Warehouse warehouse)
        {
            // Get ids of latest movements at positions
            ICollection<long> ids =
                (
                    from movement in ware.Movements
                    orderby movement.PositionId, movement.UtcCreated
                    where movement.Position.Section.Warehouse.Id == warehouse.Id
                    group movement by movement.PositionId into list
                    let id = list.Max(x => x.Id)
                    select id
                ).ToList(); //https://www.dotnetcurry.com/ShowArticle.aspx?ID=414

            // Return SUM of CountTotal of movements where id is contained in list ids
            return ware.Movements.Where(x => ids.Contains(x.Id) && x.CountTotal > 0).Sum(x => x.CountTotal);
        }
    }
}
