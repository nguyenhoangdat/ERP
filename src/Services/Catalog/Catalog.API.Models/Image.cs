using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models
{
    public class Image
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Url]
        public string Url { get; set; }

        [Required]
        public string Alt { get; set; }
    }
}
