using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models
{
    public class Parameter
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }

        [Required]
        public long ParametersGroupId { get; set; }
        public virtual ParametersGroup Group { get; set; }
    }
}
