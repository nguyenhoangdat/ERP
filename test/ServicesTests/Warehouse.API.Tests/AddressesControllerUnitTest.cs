using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Warehouse.API.Controllers;
using Warehouse.API.Models;

namespace Warehouse.API.Tests
{
    [TestClass]
    public class AddressesControllerUnitTest
    {
        private AddressesController _addressesController { get; set; }

        public AddressesControllerUnitTest()
        {
            // In-memory database only exists while the connection is open
            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseForTestingWarehouseAPI")
                .Options;

            DatabaseContext context = new DatabaseContext(options);
            context.Database.EnsureCreated();
            _addressesController = new AddressesController(context);
        }

        [TestMethod]
        public void A01_CreateAddress()
        {
            Address expected = new Address()
            {
                Street = "Jiřího nám. 20/I",
                ZipCode = "290 31",
                City = "Poděbrady",
                Country = "Czech Republic"
            };
            Address actual = new Address()
            {
                Street = "Jiřího nám. 20/I",
                ZipCode = "290 31",
                City = "Poděbrady",
                Country = "Czech Republic"
            };

            Task<IActionResult> postAddress = Task.Run(async () => {
                return await _addressesController.PostAddress(actual);
            });
            postAddress.Wait();

            Assert.IsTrue(
                expected.Street == actual.Street &&
                expected.ZipCode == actual.ZipCode &&
                expected.City == actual.City &&
                expected.Country == actual.Country);
        }

        [TestMethod]
        public void A02_UpdateAddress()
        {
            Address expected = new Address()
            {
                Id = 1,
                Street = "Plzeňská 150",
                ZipCode = "290 31",
                City = "Praha",
                Country = "Czech Republic"
            };
            Address actual = new Address()
            {
                Id = 1,
                Street = "Plzeňská 150",
                ZipCode = "290 31",
                City = "Praha",
                Country = "Czech Republic"
            };

            Task<IActionResult> putAddress = Task.Run(async () => {
                return await _addressesController.PutAddress(1, actual);
            });
            putAddress.Wait();

            Assert.IsTrue(
                expected.Street == actual.Street &&
                expected.ZipCode == actual.ZipCode &&
                expected.City == actual.City &&
                expected.Country == actual.Country);
        }

        [TestMethod]
        public void A03_GetAddress()
        {
            Address expected = new Address()
            {
                Id = 1,
                Street = "Plzeňská 150",
                ZipCode = "290 31",
                City = "Praha",
                Country = "Czech Republic"
            };

            Task<IActionResult> getAddress = Task.Run(async () => {
                return await _addressesController.GetAddress(1);
            });
            getAddress.Wait();

            Address actual = ((getAddress.Result as OkObjectResult).Value as Address);

            Assert.IsTrue(
                expected.Street == actual.Street &&
                expected.ZipCode == actual.ZipCode &&
                expected.City == actual.City &&
                expected.Country == actual.Country);
        }
    }
}
