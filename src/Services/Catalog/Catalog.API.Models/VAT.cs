using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models
{
    public class VAT
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Range(0, 1)]
        public double Value { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}