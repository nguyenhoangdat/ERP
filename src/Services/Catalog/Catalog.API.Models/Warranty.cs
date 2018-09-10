using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models
{
    public class Warranty
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Months { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}