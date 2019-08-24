using Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors
{
    public interface IReceiptPositionSelector
    {
        IEnumerable<PositionCount> GetPositions(Ware ware, int count);
    }
}
