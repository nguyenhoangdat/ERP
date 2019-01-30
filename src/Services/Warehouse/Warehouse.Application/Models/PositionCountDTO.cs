using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Models
{
    public class PositionCountDTO
    {
        public PositionCountDTO(Position position, int count)
        {
            this.Position = position;
            this.Count = count;
        }

        public Position Position { get; set; }
        public int Count { get; set; }
    }
}
