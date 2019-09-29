using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database.Configuration.Setting;
using Restmium.ERP.Services.Warehouse.Tests.Common;
using Restmium.ERP.Services.Warehouse.Tests.Common.Interfaces;
using System;
using System.Linq;

namespace Warehouse.Domain.Tests.Entities
{
    [TestClass]
    public class PositionTests
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
        public void GetWare()
        {
            Position position;

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 1);
            Assert.IsNull(position.GetWare());

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 2);
            Assert.IsNull(position.GetWare());

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 3);
            Assert.IsTrue(position.GetWare().Id == 1);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 4);
            Assert.IsFalse(position.GetWare().Id == 1);
        }
        [TestMethod, TestCategory("Extensions")]
        public void GetLastWare()
        {
            Position position;

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 1);
            Assert.IsNull(position.GetLastWare());

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 2);
            Assert.IsNull(position.GetLastWare());

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 3);
            Assert.IsTrue(position.GetLastWare().Id == 1);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 4);
            Assert.IsTrue(position.GetLastWare().Id == 2);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 5);
            Assert.IsTrue(position.GetLastWare().Id == 3);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 6);
            Assert.IsTrue(position.GetLastWare().Id == 4);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 7);
            Assert.IsNull(position.GetLastWare());

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 8);
            Assert.IsNull(position.GetLastWare());

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 9);
            Assert.IsNull(position.GetLastWare());
        }
        [TestMethod, TestCategory("Extensions")]
        public void CountWare()
        {
            Position position;

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 1);
            Assert.IsTrue(position.CountWare() == 0);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 2);
            Assert.IsTrue(position.CountWare() == 0);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 3);
            Assert.IsTrue(position.CountWare() == 9);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 4);
            Assert.IsTrue(position.CountWare() == 5);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 5);
            Assert.IsTrue(position.CountWare() == 5);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 6);
            Assert.IsTrue(position.CountWare() == 0);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 7);
            Assert.IsTrue(position.CountWare() == 0);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 8);
            Assert.IsTrue(position.CountWare() == 0);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 9);
            Assert.IsTrue(position.CountWare() == 0);
        }
        [TestMethod, TestCategory("Extensions")]
        public void CountAvailableWare()
        {
            Position position;

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 1);
            Assert.IsTrue(position.CountAvailableWare() == 0);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 3);
            Assert.IsTrue(position.CountAvailableWare() == 9);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 4);
            Assert.IsTrue(position.CountAvailableWare() == 5);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 5);
            Assert.IsTrue(position.CountAvailableWare() == 4); // 1 reserved unit

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 6);
            Assert.IsTrue(position.CountAvailableWare() == 0);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 7);
            Assert.IsTrue(position.CountAvailableWare() == 0);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 8);
            Assert.IsTrue(position.CountAvailableWare() == 0);

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 9);
            Assert.IsTrue(position.CountAvailableWare() == 0);
        }

        [TestMethod, TestCategory("Extensions")]
        public void HasCapacity()
        {
            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 3);
            Ware ware = this.DatabaseContext.Wares.FirstOrDefault(x => x.Id == 1);

            Assert.IsTrue(position.HasCapacity(ware, 120));
            Assert.IsFalse(position.HasCapacity(ware, 121));

            Assert.ThrowsException<ArgumentNullException>(() => position.HasCapacity(null, 20));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => position.HasCapacity(ware, -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => position.HasCapacity(ware, 0));
        }
        [TestMethod, TestCategory("Extensions")]
        public void HasLoadCapacity()
        {
            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 3);
            Ware ware = this.DatabaseContext.Wares.FirstOrDefault(x => x.Id == 1);

            Assert.IsTrue(position.HasLoadCapacity(ware, 120));
            Assert.IsFalse(position.HasLoadCapacity(ware, 121));

            Assert.ThrowsException<ArgumentNullException>(() => position.HasLoadCapacity(null, 20));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => position.HasLoadCapacity(ware, -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => position.HasLoadCapacity(ware, 0));
        }
        [TestMethod, TestCategory("Extensions")]
        public void HasSpaceCapacity()
        {
            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 3);
            Ware ware = this.DatabaseContext.Wares.FirstOrDefault(x => x.Id == 1);

            Assert.IsTrue(position.HasSpaceCapacity(ware, 432));
            Assert.IsFalse(position.HasSpaceCapacity(ware, 434));

            Assert.ThrowsException<ArgumentNullException>(() => position.HasSpaceCapacity(null, 20));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => position.HasSpaceCapacity(ware, -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => position.HasSpaceCapacity(ware, 0));
        }

        [TestMethod, TestCategory("Extensions")]
        public void MaxCapacity()
        {
            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 3);
            Ware ware = this.DatabaseContext.Wares.FirstOrDefault(x => x.Id == 1);

            Assert.IsTrue(position.MaxCapacity(ware) == 120);
            Assert.IsFalse(position.MaxCapacity(ware) == 121);

            Assert.ThrowsException<ArgumentNullException>(() => position.MaxCapacity(null));
        }
        [TestMethod, TestCategory("Extensions")]
        public void MaxLoadCapacity()
        {
            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 3);
            Ware ware = this.DatabaseContext.Wares.FirstOrDefault(x => x.Id == 1);

            Assert.IsTrue(position.MaxLoadCapacity(ware) == 120);
            Assert.IsFalse(position.MaxLoadCapacity(ware) == 121);

            Assert.ThrowsException<ArgumentNullException>(() => position.MaxLoadCapacity(null));
        }
        [TestMethod, TestCategory("Extensions")]
        public void MaxSpaceCapacity()
        {
            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 3);
            Ware ware = this.DatabaseContext.Wares.FirstOrDefault(x => x.Id == 1);

            Assert.IsTrue(position.MaxSpaceCapacity(ware) == 432);
            Assert.IsFalse(position.MaxSpaceCapacity(ware) == 433);

            Assert.ThrowsException<ArgumentNullException>(() => position.MaxSpaceCapacity(null));
        }

        [TestMethod, TestCategory("Extensions")]
        public void HasAllIssueSlipItemsProcessed()
        {
            Position position;

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 3);
            Assert.IsTrue(position.HasAllIssueSlipItemsProcessed());

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 5);
            Assert.IsFalse(position.HasAllIssueSlipItemsProcessed());
        }
        [TestMethod, TestCategory("Extensions")]
        public void HasAllReceiptItemsProcessed()
        {
            Position position;

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 4);
            Assert.IsTrue(position.HasAllReceiptItemsProcessed());

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 3);
            Assert.IsFalse(position.HasAllReceiptItemsProcessed());
        }
        [TestMethod, TestCategory("Extensions")]
        public void HasAllStockTakingItemsProcessed()
        {
            Position position;

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 3);
            Assert.IsTrue(position.HasAllStockTakingItemsProcessed());

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 6);
            Assert.IsFalse(position.HasAllStockTakingItemsProcessed());
        }

        [TestMethod, TestCategory("Extensions")]
        public void CanBeMovedToBin()
        {
            Position position;

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 8);
            Assert.IsTrue(position.CanBeMovedToBin());

            position.UtcMovedToBin = DateTime.UtcNow.AddMinutes(-1);
            Assert.IsFalse(position.CanBeMovedToBin());
        }
        [TestMethod, TestCategory("Extensions")]
        public void CanBeRestoredFromBin()
        {
            Position position;

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 8);
            Assert.IsFalse(position.CanBeRestoredFromBin());

            position.UtcMovedToBin = DateTime.UtcNow.AddMinutes(-1);
            Assert.IsTrue(position.CanBeRestoredFromBin());
        }

        [TestMethod, TestCategory("Extensions")]
        public void CanBeDeleted()
        {
            Position position;

            position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == 8);
            Assert.IsFalse(position.CanBeDeleted());

            position.UtcMovedToBin = DateTime.UtcNow.AddMinutes(-1);
            Assert.IsTrue(position.CanBeDeleted());
        }
    }
}
