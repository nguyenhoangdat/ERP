using System.Collections.Generic;
using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class IssueSlipExtensions
    {
        public static bool HasSectionWithId(this IssueSlip issueSlip, int sectionId)
        {
            return issueSlip.Items.FirstOrDefault(x => x.Position.SectionId == sectionId) != null;
        }

        /// <summary>
        /// Returns the unissued items within the IssueSlip
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IssueSlip.Item> GetUnissuedItems(this IssueSlip issueSlip)
        {
            return issueSlip.Items.Where(x => x.IssuedUnits < x.RequestedUnits);
        }
    }
}
