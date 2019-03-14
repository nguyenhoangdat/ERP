using Restmium.ERP.BuildingBlocks.Common.Entities;

namespace Restmium.ERP.Services.Catalog.Domain.Entities
{
    /// <summary>
    /// Represents a warranty of a product
    /// </summary>
    public class Warranty : DatabaseEntity
    {
        /// <summary>
        /// Text shown in the app or on the website
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Duration of warranty in months
        /// </summary>
        public int Duration { get; set; }
    }
}