using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities
{
    /// <summary>
    /// DTO for <see cref="Warehouse.Domain.Entities.StockTaking"/>
    /// </summary>
    public partial class StockTakingDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
