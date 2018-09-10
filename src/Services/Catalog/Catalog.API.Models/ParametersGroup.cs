using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models
{
    public class ParametersGroup
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsNameVisible { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public virtual ICollection<Parameter> Parameters { get; set; }
    }
}
