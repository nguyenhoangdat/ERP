using Restmium.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Restmium.Models.Cards
{
    public partial class IsicCard : DatabaseEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CardId { get; set; }
    }
}
