using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.API.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        #region General info
        [Required]
        public string Name { get; set; }

        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        [Required]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [Required]
        public int WarrantyId { get; set; }
        public virtual Warranty Warranty { get; set; }
        #endregion

        #region Prices
        [Required, Range(0, double.MaxValue)]
        public decimal MinimumPrice { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int VATId { get; set; }
        public virtual VAT VAT { get; set; }
        #endregion

        #region Units
        [Required]
        public int AvailableUnits { get; set; }

        [Required]
        public int ReservedUnits { get; set; }
        public virtual ICollection<BasketReservation> BasketReservations { get; set; }

        /// <summary>
        /// Sold and not paid units
        /// </summary>
        [Required]
        public int SoldUnits { get; set; }

        [NotMapped]
        public int Units => AvailableUnits + ReservedUnits + SoldUnits;
        #endregion

        #region Organizational info
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        #endregion

        [Required, Url]
        public string UrlAddress { get; set; }

        [Required]
        public bool IsVisible { get; set; }

        public virtual ICollection<ParametersGroup> ParametersGroups { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
