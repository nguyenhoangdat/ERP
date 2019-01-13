using Restmium.ERP.BuildingBlocks.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities.Abstract
{
    public abstract class WarePosition : DatabaseEntity
    {
        [Required]
        public int WareId { get; set; }
        public virtual Ware Ware { get; set; }

        [Required]
        public long PositionId { get; set; }
        public virtual Position Position { get; set; }
    }
}
