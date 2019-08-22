using System.Collections.Generic;
using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class IssueSlipExtensions
    {
        public static bool HasSectionId(this IssueSlip issueSlip, int sectionId)
        {
            return issueSlip.Items.FirstOrDefault(x => x.Position.SectionId == sectionId) != null;
        }

        public static bool HasSectionIdWithUnissuedUnits(this IssueSlip issueSlip, int sectionId)
        {
             return GetFirstItemInSectionWithUnissuedUnits(issueSlip, sectionId) != null;
        }

        public static IssueSlip.Item GetFirstItemInSectionWithUnissuedUnits(this IssueSlip issueSlip, int sectionId)
        {
            return issueSlip.Items.FirstOrDefault(x =>
                x.Position.SectionId == sectionId &&
                x.IssuedUnits < x.RequestedUnits);
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
