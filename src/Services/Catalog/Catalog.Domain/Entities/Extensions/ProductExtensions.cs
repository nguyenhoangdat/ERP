using System;
using System.Linq;

namespace Restmium.ERP.Services.Catalog.Domain.Entities.Extensions
{
    /// <summary>
    /// Extension methods for Product
    /// </summary>
    public static class ProductExtensions
    {
        /// <summary>
        /// Gets the current price of the product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>The current price</returns>
        public static PriceListItem GetPrice(this Product product)
        {
            return product.Prices.Where(x =>
                x.UtcApplyFrom <= DateTime.UtcNow &&
                (x.UtcApplyTo >= DateTime.UtcNow || x.UtcApplyTo == null)
            ).OrderBy(x => x.UtcApplyTo).FirstOrDefault();
        }
    }
}
