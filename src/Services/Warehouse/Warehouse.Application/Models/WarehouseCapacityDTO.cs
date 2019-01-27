using System;
using System.Collections.Generic;
using System.Text;

namespace Restmium.ERP.Services.Warehouse.Application.Models
{
    public class WarehouseCapacityDTO
    {
        public long UsedPositions { get; set; }
        public long FreePositions { get; set; }
        public long TotalPositions { get; set; }
    }
}
