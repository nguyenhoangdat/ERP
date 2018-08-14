using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ordering.API.Models
{
    public class OrderAction
    {
        [Required]
        public long OrderId { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public string Note { get; set; }

        
    }
}
