using Restmium.ERP.BuildingBlocks.Common.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Catalog.Domain.Entities
{
    public class Brand : DatabaseEntity
    {
        public Brand()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; protected set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; protected set; }
    }
}
