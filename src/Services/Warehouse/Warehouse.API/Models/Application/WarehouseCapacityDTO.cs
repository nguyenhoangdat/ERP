namespace Restmium.ERP.Services.Warehouse.API.Models
{
    public class WarehouseCapacityDTO
    {
        public long UsedPositions { get; set; }
        public long FreePositions { get; set; }
        public long TotalPositions { get; set; }
    }
}
