using Restmium.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Restmium.Models.Geo
{
    public class Address : DatabaseEntity
    {
        public Address()
        {

        }
        public Address(string street, string city, string country, string zipcode) : this(street, city, null, country, zipcode, null)
        {

        }
        public Address(string street, string city, string state, string country, string zipcode, string pobox)
        {
            this.Street = street;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.ZipCode = zipcode;
            this.POBox = pobox;
        }

        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string ZipCode { get; set; }       

        public string POBox { get; set; } //Schránka na poště
    }
}
