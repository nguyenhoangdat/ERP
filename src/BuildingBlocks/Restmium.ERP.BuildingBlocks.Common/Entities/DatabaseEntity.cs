using System;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.BuildingBlocks.Common.Entities
{
    /// <summary>
    /// Represents a parental class for all database entities with mandatory properties.
    /// </summary>
    public abstract class DatabaseEntity
    {
        /// <summary>
        /// DateTime when the entity was created
        /// </summary>
        [Required]
        public DateTime UtcCreated { get; set; }

        /// <summary>
        /// DateTime when the entity should be deleted.
        /// </summary>
        public DateTime? UtcDelete { get; set; }

        /// <summary>
        /// DateTime when the entity was moved to the bin. Entity should be removed after 30 days in the bin.
        /// </summary>
        public DateTime? UtcMovedToBin { get; set; }
    }
}
