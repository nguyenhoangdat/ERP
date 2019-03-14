using Restmium.ERP.BuildingBlocks.Common.Entities;
using System;

namespace Restmium.ERP.Services.Catalog.Domain.Entities
{
    public class PriceListItem : DatabaseEntity
    {
        /// <summary>
        /// Mimimum price for which the product can be sold
        /// </summary>
        public decimal MinimumPrice { get; set; }
        /// <summary>
        /// Price recommended by manufacturer
        /// </summary>
        public decimal RecommendedPrice { get; set; }
        /// <summary>
        /// Standard price by a company
        /// </summary>
        public decimal StandardPrice { get; set; }
        /// <summary>
        /// Up to date price
        /// </summary>
        public decimal CurrentPrice { get; set; }
        /// <summary>
        /// Flags indicating aditional information about the price (Discount, Sale, ...)
        /// </summary>
        public PriceFlags PriceFlags { get; set; }

        /// <summary>
        /// The UTC date from when the price will be applied
        /// </summary>
        public DateTime UtcApplyFrom { get; set; }
        /// <summary>
        /// The UTC date to which the price will be applied
        /// </summary>
        public DateTime? UtcApplyTo { get; set; }
    }
}
