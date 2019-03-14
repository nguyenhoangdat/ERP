using Restmium.ERP.BuildingBlocks.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restmium.ERP.Services.Catalog.Domain.Entities
{
    public class Product : DatabaseEntity
    {
        //TODO: Add localization support for texts
        public Product()
        {
            this.ParametersGroups = new HashSet<ParametersGroup>();
            this.Prices = new HashSet<PriceListItem>();
            this.Reservations = new HashSet<Reservation>();
        }

        #region Basic information
        public int Id { get; protected set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        public ICollection<ParametersGroup> ParametersGroups { get; protected set; }
        //TODO: Add Images and Files

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        #endregion

        #region Prices
        public virtual ICollection<PriceListItem> Prices { get; protected set; }
        #endregion

        #region Legal information
        public Warranty Warranty { get; set; }
        #endregion
        
        #region Availability
        /// <summary>
        /// Number of Available units
        /// </summary>
        public int AvailableUnits { get; set; }
        /// <summary>
        /// Number of units that have reservation in Basket.API
        /// </summary>
        public int ReservedUnits { get; set; }
        /// <summary>
        /// Number of units that have been sold but not are paid yet
        /// </summary>
        public int SoldUnits { get; set; }

        /// <summary>
        /// Number of units that are owned by the company
        /// </summary>
        [NotMapped]
        public int UnitsTotal => this.AvailableUnits + this.ReservedUnits + this.SoldUnits;

        public virtual ICollection<Reservation> Reservations { get; set; }
        #endregion

        #region System information
        public bool IsVisible { get; set; }
        /// <summary>
        /// URL identifier.
        /// </summary>
        public string UrlIdentifier { get; set; }
        #endregion

        #region Organizational info
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        #endregion
    }
}
