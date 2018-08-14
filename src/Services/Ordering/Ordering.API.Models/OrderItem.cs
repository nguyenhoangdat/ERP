using System.ComponentModel.DataAnnotations;

namespace Ordering.API.Models
{
    public class OrderItem
    {
        [Required, Range(0, int.MaxValue)]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required, Range(0, int.MaxValue)]
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required, Range(0, 1)]
        public double VAT { get; set; }

        [Range(0, 1)]
        public double Discount { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Ammount { get; set; }

        [Required]
        public int WarrantyMonths { get; set; }
    }
}
