using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities
{
    /// <summary>
    /// DTO for <see cref="Warehouse.Domain.Entities.Address"/>
    /// </summary>
    public class AddressDTO
    {
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string ZipCode { get; set; }
    }
}