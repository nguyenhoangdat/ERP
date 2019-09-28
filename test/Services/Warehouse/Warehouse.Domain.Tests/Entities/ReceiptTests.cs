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
    public class ReceiptTests
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
            Receipt receipt;

            // Not processed
            receipt = this.DatabaseContext.Receipts.FirstOrDefault(x => x.Id == 2);
            Assert.IsTrue(receipt.CanBeMovedToBin());

            // Processed, retention period didn't pass
            receipt = this.DatabaseContext.Receipts.FirstOrDefault(x => x.Id == 1);
            Assert.IsFalse(receipt.CanBeMovedToBin());

            // Already in a bin
            receipt = this.DatabaseContext.Receipts.FirstOrDefault(x => x.Id == 3);
            Assert.IsFalse(receipt.CanBeMovedToBin());
        }
        [TestMethod, TestCategory("Extensions")]
        public void CanBeRestoredFromBin()
        {
            Receipt receipt;

            // Already in a bin
            receipt = this.DatabaseContext.Receipts.FirstOrDefault(x => x.Id == 3);
            Assert.IsTrue(receipt.CanBeRestoredFromBin());

            // Processed, retention period didn't pass, not in a bin
            receipt = this.DatabaseContext.Receipts.FirstOrDefault(x => x.Id == 1);
            Assert.IsFalse(receipt.CanBeRestoredFromBin());

            // Not processed, not in a bin
            receipt = this.DatabaseContext.Receipts.FirstOrDefault(x => x.Id == 2);
            Assert.IsFalse(receipt.CanBeRestoredFromBin());
        }
    }
}
