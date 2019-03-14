using System;

namespace Restmium.ERP.Services.Catalog.Domain.Entities
{
    /// <summary>
    /// Price flags indicating additional information about the product's price
    /// </summary>
    [Flags]
    public enum PriceFlags
    {
        /// <summary>
        /// Normal price by the company
        /// </summary>
        Normal = 0,
        /// <summary>
        /// Product has discount
        /// </summary>
        Discount = 1,
        /// <summary>
        /// Product is on sale
        /// </summary>
        Sale = 2
    }
}
