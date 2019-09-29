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
    public class ReceiptItemTests
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
            Receipt.Item item;

            // Not processed
            item = this.DatabaseContext.ReceiptItems
                .FirstOrDefault(x =>
                    x.ReceiptId == 2 &&
                    x.PositionId == 3 &&
                    x.WareId == 1);
            Assert.IsTrue(item.CanBeMovedToBin());

            // Processed, retention period didn't pass
            item = this.DatabaseContext.ReceiptItems
                .FirstOrDefault(x =>
                    x.ReceiptId == 1 &&
                    x.PositionId == 3 &&
                    x.WareId == 1);
            Assert.IsFalse(item.CanBeMovedToBin());
        }
        [TestMethod, TestCategory("Extensions")]
        public void CanBeRestoredFromBin()
        {
            Receipt.Item item;

            // Not processed - in a bin
            item = this.DatabaseContext.ReceiptItems
                .FirstOrDefault(x =>
                    x.ReceiptId == 3 &&
                    x.PositionId == 1 &&
                    x.WareId == 2);
            item.Receipt.UtcMovedToBin = null;
            Assert.IsTrue(item.CanBeRestoredFromBin());

            // Processed - not in a bin
            item = this.DatabaseContext.ReceiptItems
                .FirstOrDefault(x =>
                    x.ReceiptId == 1 &&
                    x.PositionId == 3 &&
                    x.WareId == 1);
            Assert.IsFalse(item.CanBeRestoredFromBin());
        }
    }
}
