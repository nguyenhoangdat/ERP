using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions
{
    public static class IssueSlipExtensions
    {
        public static Section GetSection(this IssueSlip issueSlip)
        {
            return issueSlip.Items.FirstOrDefault().Position.Section;
        }
    }
}
