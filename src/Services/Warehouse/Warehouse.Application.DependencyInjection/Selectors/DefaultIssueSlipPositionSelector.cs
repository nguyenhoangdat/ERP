using Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors
{
    public class DefaultIssueSlipPositionSelector : IIssueSlipPositionSelector
    {
        public DefaultIssueSlipPositionSelector(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public IEnumerable<PositionCount> GetPositions(Ware ware, int count)
        {
            List<PositionCount> positionCounts = new List<PositionCount>();

            IQueryable<Position> positions = this.DatabaseContext.Positions.Where(x => x.GetWare().Id == ware.Id);

            if (positions.Any(x => x.CountAvailableWare() >= count))
            {
                // Take the smallest position that can be used to issue the units ordered
                Position position = positions.Where(x => x.CountAvailableWare() >= count).OrderBy(x => x.CountAvailableWare()).FirstOrDefault();
                positionCounts.Add(new PositionCount(position, count));
            }
            else
            {
                foreach (Position item in positions.OrderByDescending(x => x.CountAvailableWare()))
                {
                    int positionCount = item.CountAvailableWare();
                    positionCounts.Add(new PositionCount(item, (count <= positionCount) ? count : positionCount));
                    count -= positionCount;
                }
            }

            return positionCounts;
        }
    }
}
