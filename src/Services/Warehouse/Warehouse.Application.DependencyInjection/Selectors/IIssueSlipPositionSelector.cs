using Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors.Models;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors
{
    public interface IIssueSlipPositionSelector
    {
        IEnumerable<PositionCount> GetPositions(int wareId, int count);
    }
}
