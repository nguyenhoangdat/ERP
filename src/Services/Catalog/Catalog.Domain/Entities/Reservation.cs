using Restmium.ERP.BuildingBlocks.Common.Entities;
using System;

namespace Restmium.ERP.Services.Catalog.Domain.Entities
{
    public class Reservation : DatabaseEntity
    {
        protected Reservation()
        {

        }
        public Reservation(int productId, int count) : this()
        {
            this.ProductId = productId;
            this.Count = count;
        }
        public Reservation(int productId, int basketId, int count) : this(productId, count)
        {
            this.BaskedId = basketId;
        }

        public long Id { get; protected set; }

        public int ProductId { get; protected set; }
        public virtual Product Product { get; protected set; }

        /// <summary>
        /// Id of Basket in Basket.API
        /// </summary>
        public int BaskedId { get; protected set; }

        /// <summary>
        /// Number of units reserved
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Time (UTC) when the reservation was create by user
        /// </summary>
        public DateTime UtcReservation { get; protected set; }
        /// <summary>
        /// Time (UTC) when the reservation expires
        /// </summary>
        public DateTime UtcExpiration { get; protected set; }
    }
}
