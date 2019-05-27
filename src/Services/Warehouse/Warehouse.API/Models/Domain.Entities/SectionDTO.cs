using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities
{
    /// <summary>
    /// DTO for <see cref="Warehouse.Domain.Entities.Section"/>
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