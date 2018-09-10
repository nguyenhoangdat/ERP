using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}