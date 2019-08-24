using Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors
{
    public class DefaultReceiptPositionSelector : IReceiptPositionSelector
    {
        public DefaultReceiptPositionSelector(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public IEnumerable<PositionCount> GetPositions(Ware ware, int count)
        {
            if (ware == null)
            {
                throw new ArgumentNullException(nameof(ware));
            }
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            List<PositionCount> positionCounts = new List<PositionCount>();

            // Positions that can hold more units
            List<Position> positionsContainingWare = this.DatabaseContext.Positions
                .Where(x =>
                    x.GetWare().Id == ware.Id &&
                    x.CountWare() < x.MaxCapacity(x.GetWare())
                ).ToList();

            count = this.ProcessPositions(positionCounts, positionsContainingWare, ware, count);

            if (count > 0)
            {
                // Positions that holded Ware as the last item and now are empty
                List<Position> positionsThatHoldedWare = this.DatabaseContext.Positions
                    .Where(x =>
                        x.CountWare() == 0 &&
                        x.GetLastWare().Id == ware.Id &&
                        x.CountWare() < x.MaxCapacity(ware)
                    ).ToList();
                count = this.ProcessPositions(positionCounts, positionsThatHoldedWare, ware, count);
            }

            if (count > 0)
            {
                // All empty positions that can hold at least one unit
                List<Position> emptyPositions = this.DatabaseContext.Positions
                    .Where(x =>
                        x.GetWare() == null &&
                        x.MaxCapacity(ware) > 0
                    ).ToList();
                count = this.ProcessPositions(positionCounts, emptyPositions, ware, count);
            }

            return positionCounts;
        }

        private int ProcessPositions(List<PositionCount> positionCounts, List<Position> positions, Ware ware, int count)
        {
            for (int i = 0; i < positions.Count() && count > 0; i++)
            {
                int numberOfUnits = Math.Min(count, positions[i].MaxCapacity(ware) - positions[i].CountWare());
                count -= numberOfUnits;
                positionCounts.Add(new PositionCount(positions[i], numberOfUnits));
            }

            return count;
        }
    }
}
