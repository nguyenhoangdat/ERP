using Restmium.ERP.Services.Warehouse.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public partial class Movement : WarePosition
    {
        public Movement()
        {

        }
        private Movement(Direction direction, EntryContent content, int countChange, int countTotal) : this()
        {
            this.MovementDirection = direction;
            this.Content = content;
            this.CountChange = countChange;
            this.CountTotal = countTotal;
        }
        public Movement(int wareId, long positionId, Direction direction, EntryContent content, int countChange, int countTotal) : this(direction, content, countChange, countTotal)
        {
            this.WareId = wareId;
            this.PositionId = positionId;
        }
        public Movement(Ware ware, Position position, Direction direction, EntryContent content, int countChange, int countTotal) : this(direction, content, countChange, countTotal)
        {
            this.Ware = ware;
            this.Position = position;
        }

        [Required]
        public long Id { get; set; }

        [Required]
        public Direction MovementDirection { get; set; }

        [Required]
        public EntryContent Content { get; set; }

        [Required]
        public int CountChange { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int CountTotal { get; set; }
    }

    public partial class Movement
    {
        public enum Direction
        {
            In,
            Out
        }

        // TODO: Consider whether this enum is useful
        public enum EntryContent
        {
            Receipt, //příjem
            Delivery, //výdej
            PositionTransfer //převod mezi pozicemi
        }
    }
}
