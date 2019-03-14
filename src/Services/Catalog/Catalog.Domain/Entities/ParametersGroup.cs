using Restmium.ERP.BuildingBlocks.Common.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Catalog.Domain.Entities
{
    public class ParametersGroup : DatabaseEntity
    {
        public ParametersGroup()
        {
            this.Parameters = new HashSet<Parameter>();
        }

        public long Id { get; protected set; }
        public string Name { get; set; }
        public bool IsNameVisible { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public virtual ICollection<Parameter> Parameters { get; protected set; }
    }
}
