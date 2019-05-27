using Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.API.Models.Application
{
    //TODO: Replace with more reasonable DTO
    public class PositionCountDTO
    {
        public PositionCountDTO()
        {

        }
        public PositionCountDTO(PositionDTO position, int count) : this()
        {
            this.Position = position;
            this.Count = count;
        }

        public PositionDTO Position { get; set; }
        public int Count { get; set; }
    }
}
