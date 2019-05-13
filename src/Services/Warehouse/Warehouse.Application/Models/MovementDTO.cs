using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Application.Models
{
    /// <summary>
    /// DTO for <see cref="Domain.Entities.Movement"/>
    /// </summary>
    public partial class MovementDTO
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public int WareId { get; set; }

        [Required]
        public long PositionId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DirectionDTO MovementDirection { get; set; }

        [Required]
        public int CountChange { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int CountTotal { get; set; }
    }

    public partial class MovementDTO
    {
        public enum DirectionDTO
        {
            In,
            Out
        }
    }
}
