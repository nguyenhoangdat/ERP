using System;
using System.ComponentModel.DataAnnotations;

namespace Restmium.Models.Abstract
{
    public abstract class DatabaseEntity
    {
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
