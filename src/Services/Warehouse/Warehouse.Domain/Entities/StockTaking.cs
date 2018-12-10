using Restmium.ERP.BuildingBlocks.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public partial class StockTaking : DatabaseEntity
    {
        public StockTaking()
        {
            this.Items = new HashSet<Item>();
        }
        public StockTaking(string name) : this()
        {
            this.Name = name;
        }

        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; protected set; }
    }
}
