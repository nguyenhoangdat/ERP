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

        public IEnumerable<PositionCount> GetPositions(int wareId, int count)
        {
            List<PositionCount> positionCounts = new List<PositionCount>();

            IQueryable<Position> positions = this.DatabaseContext.Positions.Where(x => x.GetWare().Id == wareId);

            if (positions.Any(x => x.CountWare() >= count))
            {
                Position position = positions.Where(x => x.CountWare() >= count).OrderBy(x => x.CountWare()).FirstOrDefault();
                positionCounts.Add(new PositionCount(position, count));
            }
            else
            {
                foreach (Position item in positions.OrderByDescending(x => x.CountWare()))
                {
                    int positionCount = item.CountWare();
                    positionCounts.Add(new PositionCount(item, (count <= positionCount) ? count : positionCount));
                    count -= positionCount;
                }
            }

            return positionCounts;
        }
    }
}
