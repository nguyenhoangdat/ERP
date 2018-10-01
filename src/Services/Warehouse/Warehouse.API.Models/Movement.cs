using Restmium.Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.API.Models
{
    public partial class Movement : DatabaseEntity
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public int WareId { get; set; }
        public virtual Ware Ware { get; set; }

        [Required]
        public long PositionId { get; set; }
        public virtual Position Position { get; set; }

        [Required]
        public Direction MovementDirection { get; set; }

        [Required]
        public EntryContent Content { get; set; }

        [Required]
        public int CountChange { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int CountTotal { get; set; }

        public DateTime? DateMoved { get; set; }
    }
}
