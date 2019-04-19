using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Models
{
    /// <summary>
    /// DTO representing Receipt entity
    /// </summary>
    public class ReceiptDto
    {
        //TODO: Add supplier & transport details (contact, ...)

        /// <summary>
        /// UTC DateTime when the goods are expected
        /// </summary>
        public DateTime UtcExpected { get; set; }
        /// <summary>
        /// A collection of Items on the Receipt
        /// </summary>
        public ICollection<Item> Items { get; set; }

        /// <summary>
        /// Item in the Receipt
        /// </summary>
        public class Item
        {
            /// <summary>
            /// Creates a new instance of <see cref="Item"/>.
            /// </summary>
            /// <param name="productId">Product Id</param>
            /// <param name="countOrdered">Number of received units</param>
            public Item(int productId, int countOrdered)
            {
                this.ProductId = productId;
                this.CountOrdered = countOrdered;
            }
            /// <summary>
            /// Creates a new instance of <see cref="Item"/>.
            /// </summary>
            /// <param name="productId">Product Id</param>
            /// <param name="countOrdered">Number of received units</param>
            /// <param name="countReceived">Number of received units</param>
            public Item(int productId, int countOrdered, int countReceived) : this(productId, countOrdered)
            {
                this.CountReceived = countReceived;
            }

            /// <summary>
            /// Id of the Product in Catalog.API
            /// </summary>
            public int ProductId { get; set; }
            /// <summary>
            /// Number of ordered units
            /// </summary>
            public int CountOrdered { get; set; }
            /// <summary>
            /// Number of received units
            /// </summary>
            public int? CountReceived { get; set; }
        }
    }
}
