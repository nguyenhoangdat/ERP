using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Application.Models
{
    /// <summary>
    /// DTO for <see cref="Domain.Entities.Position"/>
    /// </summary>
    public class PositionDTO
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public double Width { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public double Depth { get; set; }
        [Required]
        public double MaxWeight { get; set; }
        public int ReservedUnits { get; set; }

        [Required]
        public int SectionId { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}