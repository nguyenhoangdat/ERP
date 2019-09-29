using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database.Configuration.Setting;
using Restmium.ERP.Services.Warehouse.Tests.Common;
using Restmium.ERP.Services.Warehouse.Tests.Common.Interfaces;
using System.Linq;

namespace Warehouse.Domain.Tests.Entities
{
    [TestClass]
    public class SectionTests
    {
        private DatabaseContext DatabaseContext { get; set; }
        private IDbSeeder DbSeeder { get; set; } = new DatabaseContextSeeder();

        [TestInitialize]
        public void TestInitialize()
        {
            SqliteConnection connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite(connection)
                .Options;

            // Create the schema in the database
            this.DatabaseContext = new DatabaseContext(options, new MovementSetting(monthsRetentionPeriod: 0));
            this.DatabaseContext.Database.EnsureCreated();

            this.DbSeeder.Seed(this.DatabaseContext);
        }
        [TestCleanup]
        public void TestCleanup()
        {
            this.DatabaseContext.Database.CloseConnection();
        }

        [TestMethod, TestCategory("Extensions")]
        public void CanBeMovedToBin()
        {
            Restmium.ERP.Services.Warehouse.Domain.Entities.Warehouse warehouse;
            warehouse = this.DatabaseContext.Warehouses.FirstOrDefault(x => x.Id == 3);

            Section section;

            section = warehouse.Sections.First(x => x.UtcMovedToBin != null);
            Assert.IsFalse(section.CanBeMovedToBin());
            
            section.UtcMovedToBin = null;
            Assert.IsTrue(section.CanBeMovedToBin());

            section = this.DatabaseContext.Sections.FirstOrDefault(x => x.Id == 2);
            Assert.IsFalse(section.CanBeMovedToBin());
        }
        [TestMethod, TestCategory("Extensions")]
        public void CanBeRestoredFromBin()
        {
            Restmium.ERP.Services.Warehouse.Domain.Entities.Warehouse warehouse;

            warehouse = this.DatabaseContext.Warehouses.FirstOrDefault(x => x.Id == 3);
            warehouse.UtcMovedToBin = null;
            Section section = warehouse.Sections.First(x => x.UtcMovedToBin != null);
            Assert.IsTrue(section.CanBeRestoredFromBin());

            Assert.IsFalse(this.DatabaseContext.Sections.FirstOrDefault(x => x.Id == 1).CanBeRestoredFromBin());
        }

        [TestMethod, TestCategory("Extensions")]
        public void CanBeDeleted()
        {
            Restmium.ERP.Services.Warehouse.Domain.Entities.Warehouse warehouse;
            warehouse = this.DatabaseContext.Warehouses.FirstOrDefault(x => x.Id == 3);

            Section section;

            section = warehouse.Sections.First(x => x.UtcMovedToBin != null);
            Assert.IsTrue(section.CanBeDeleted());

            section = this.DatabaseContext.Sections.FirstOrDefault(x => x.Id == 1);
            Assert.IsFalse(section.CanBeDeleted());
        }
    }
}
