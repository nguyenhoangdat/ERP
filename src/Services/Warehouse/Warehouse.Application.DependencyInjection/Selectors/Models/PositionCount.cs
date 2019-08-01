using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors.Models
{
    public class PositionCount
    {
        public PositionCount(Position position, int count)
        {
            this.Position = position;
            this.Count = count;
        }

        public Position Position { get; }
        public int Count { get; }
    }
}
