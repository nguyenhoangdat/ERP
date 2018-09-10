using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        //TODO: Add subcategories
        //[Required]
        //public int ParentCategoryId { get; set; }
        //public virtual Category ParentalCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        //public virtual ICollection<Category> Subcategories { get; set; } //TODO: Fluent API mapping + OnDelete()
    }
}
