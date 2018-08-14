using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ordering.API.Models
{
    public class Order
    {
        [Required]
        public long Id { get; set; }
        
        [Required]
        public long InvoiceInfoId { get; set; }
        public virtual InvoiceInfo InvoiceInfo { get; set; }
        
        [Required]
        public long ShippingInfoId { get; set; }
        public virtual ShippingInfo ShippingInfo { get; set; }

        [Required]
        public DateTime UtcExpireAt { get; set; } // Datum expirace - navrácení zboží do skladu

        public virtual ICollection<OrderItem> Items { get; set; }
        public virtual ICollection<OrderAction> OrderActions { get; set; }
    }
}
