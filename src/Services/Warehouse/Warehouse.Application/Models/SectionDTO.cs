using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Application.Models
{
    /// <summary>
    /// DTO for <see cref="Domain.Entities.Section"/>
    /// </summary>
    public class SectionDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int WarehouseId { get; set; }
    }
}