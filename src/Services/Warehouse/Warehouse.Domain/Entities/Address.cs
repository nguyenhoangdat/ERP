using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public class Address
    {
        public Address()
        {

        }
        public Address(string street, string city, string coutry, string zipCode) : this()
        {
            this.Street = street;
            this.City = city;
            this.Country = coutry;
            this.ZipCode = zipCode;
        }
        public Address(string street, string city, string state, string coutry, string zipCode) : this(street, city, coutry, zipCode)
        {
            this.State = state;
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
    }
}
