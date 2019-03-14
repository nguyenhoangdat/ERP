using Restmium.ERP.BuildingBlocks.Common.Entities;

namespace Restmium.ERP.Services.Catalog.Domain.Entities
{
    public class VAT : DatabaseEntity
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }
}
