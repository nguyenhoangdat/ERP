using Restmium.ERP.BuildingBlocks.Common.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Catalog.Domain.Entities
{
    public class Category : DatabaseEntity
    {
        public int Id { get; protected set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; protected set; } = new HashSet<Product>();
    }
}
