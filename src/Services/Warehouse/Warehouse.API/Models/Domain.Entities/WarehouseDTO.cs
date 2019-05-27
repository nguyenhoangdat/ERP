using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities
{
    /// <summary>
    /// DTO for <see cref="Warehouse.Domain.Entities.Warehouse"/>
    /// </summary>
    public class WarehouseDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public AddressDTO Address { get; set; }
    }
}
