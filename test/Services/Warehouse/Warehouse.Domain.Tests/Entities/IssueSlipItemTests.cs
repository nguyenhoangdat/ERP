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
    public class IssueSlipItemTests
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

        [TestMethod, TestCategory("Entity")]
        public void ConstructorTest1()
        {
            IssueSlip.Item item = new IssueSlip.Item(
                issueSlipId: 10,
                wareId: 3,
                positionId: 2,
                requestedUnits: 10,
                issuedUnits: 4);
            Assert.IsTrue(
                item.IssueSlipId == 10 &&
                item.WareId == 3 &&
                item.PositionId == 2 &&
                item.RequestedUnits == 10 &&
                item.IssuedUnits == 4);
        }

        [TestMethod, TestCategory("Extensions")]
        public void CanBeMovedToBin()
        {
            IssueSlip.Item item;

            item = this.DatabaseContext.IssueSlipItems.FirstOrDefault(x =>
                x.IssueSlip.OrderId == 2 &&
                x.IssuedUnits == x.RequestedUnits);
            Assert.IsTrue(item.CanBeMovedToBin());

            item = this.DatabaseContext.IssueSlipItems.FirstOrDefault(x =>
                x.IssueSlip.OrderId == 3 &&
                x.IssuedUnits == x.RequestedUnits);
            Assert.IsFalse(item.CanBeMovedToBin());

            item = this.DatabaseContext.IssueSlipItems.FirstOrDefault(x =>
                x.IssueSlip.OrderId == 1 &&
                x.IssuedUnits < x.RequestedUnits);
            Assert.IsFalse(item.CanBeMovedToBin());
        }

        [TestMethod, TestCategory("Extensions")]
        public void CanBeRestoredFromBin()
        {
            IssueSlip.Item item;

            item = this.DatabaseContext.IssueSlipItems.FirstOrDefault(x =>
                x.IssueSlip.OrderId == 4 &&
                x.UtcMovedToBin != null);
            Assert.IsTrue(item.CanBeRestoredFromBin());

            item = this.DatabaseContext.IssueSlipItems.FirstOrDefault(x =>
                x.IssueSlip.OrderId == 4 &&
                x.UtcMovedToBin == null);
            Assert.IsFalse(item.CanBeRestoredFromBin());
        }
    }
}
