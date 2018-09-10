using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models
{
    public class BasketReservation
    {
        [Required]
        public long Id { get; set; }

        /// <summary>
        /// Foreign key to Basket.API
        /// </summary>
        [Required]
        public int BasketId { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public int Units { get; set; }

        public DateTime UtcReservationTime { get; set; }
        public DateTime UtcExpirationTime { get; set; }
    }
}
