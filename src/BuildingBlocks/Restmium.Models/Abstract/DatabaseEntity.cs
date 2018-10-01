using System;
using System.ComponentModel.DataAnnotations;

namespace Restmium.Models.Abstract
{
    public abstract class DatabaseEntity
    {
        [Required]
        public DateTime UtcCreatedAt { get; set; }
        public DateTime UtcDeletedAt { get; set; }
    }
}
