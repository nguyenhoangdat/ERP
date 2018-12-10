using Microsoft.EntityFrameworkCore;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database.Extensions
{
    public static class DbSetIssueSlipExtension
    {
        /// <summary>
        /// Returns the unissued items within the IssueSlip
        /// </summary>
        /// <param name="dbSet">The instance of DbSet</param>
        /// <param name="issueSlip">The instance of IssueSlip</param>
        /// <returns></returns>
        public static ICollection<IssueSlip.Item> GetUnissuedItems(this DbSet<IssueSlip.Item> dbSet, IssueSlip issueSlip)
        {
            return issueSlip.Items.Where(x => x.IssuedUnits < x.RequestedUnits).ToList();
        }
    }
}
