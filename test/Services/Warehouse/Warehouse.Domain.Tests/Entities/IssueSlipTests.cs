using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Tests.Common;
using Restmium.ERP.Services.Warehouse.Tests.Common.Interfaces;

namespace Warehouse.Domain.Tests.Entities
{
    [TestClass]
    public class IssueSlipTests
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
            this.DatabaseContext = new DatabaseContext(options);
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
            IssueSlip issueSlip = new IssueSlip();
            Assert.IsNotNull(issueSlip.Items);
        }
        [TestMethod, TestCategory("Entity")]
        public void ConstructorTest2()
        {
            DateTime utcDispatchDate = DateTime.UtcNow.AddHours(4);
            DateTime utcDeliveryDate = DateTime.UtcNow.AddHours(8);
            IssueSlip issueSlip = new IssueSlip(1, utcDispatchDate, utcDeliveryDate);

            Assert.IsTrue(
                issueSlip.OrderId == 1 &&
                issueSlip.UtcDeliveryDate == utcDeliveryDate &&
                issueSlip.UtcDispatchDate == utcDispatchDate &&
                issueSlip.Items != null);
        }
        [TestMethod, TestCategory("Entity")]
        public void ConstructorTest3()
        {
            List<IssueSlip.Item> items = new List<IssueSlip.Item>
            {
                new IssueSlip.Item(0, 1, 1, 10, 0)
            };

            DateTime utcDispatchDate = DateTime.UtcNow.AddHours(4);
            DateTime utcDeliveryDate = DateTime.UtcNow.AddHours(8);
            IssueSlip issueSlip = new IssueSlip(1, utcDispatchDate, utcDeliveryDate, items);

            Assert.IsTrue(
                issueSlip.OrderId == 1 &&
                issueSlip.UtcDeliveryDate == utcDeliveryDate &&
                issueSlip.UtcDispatchDate == utcDispatchDate &&
                issueSlip.Items.Count == items.Count);
        }

        [TestMethod, TestCategory("Extensions")]
        public void HasSectionIdTest()
        {
            IssueSlip issueSlip = this.DatabaseContext.IssueSlips.FirstOrDefault(x => x.OrderId == 1);

            Assert.IsTrue(issueSlip.HasSectionId(2));

            Assert.IsFalse(issueSlip.HasSectionId(0));
            Assert.IsFalse(issueSlip.HasSectionId(1)); // Unassigned position (system)
            Assert.IsFalse(issueSlip.HasSectionId(3));
        }

        [TestMethod, TestCategory("Extensions")]
        public void HasSectionIdWithUnissuedUnitsTest()
        {
            IssueSlip issueSlip = this.DatabaseContext.IssueSlips.FirstOrDefault(x => x.OrderId == 1);

            Assert.IsTrue(issueSlip.HasSectionIdWithUnissuedUnits(2)); // Exists

            Assert.IsFalse(issueSlip.HasSectionIdWithUnissuedUnits(0)); // Doesn't exit
            Assert.IsFalse(issueSlip.HasSectionIdWithUnissuedUnits(1)); // Unassigned position (system)

            Assert.IsFalse(issueSlip.HasSectionIdWithUnissuedUnits(3));
        }

        [TestMethod, TestCategory("Extensions")]
        public void GetFirstItemInSectionWithUnissuedUnits()
        {
            IssueSlip issueSlip = this.DatabaseContext.IssueSlips.FirstOrDefault(x => x.OrderId == 1);
            IssueSlip.Item item = this.DatabaseContext.IssueSlipItems.FirstOrDefault(x => 
                x.IssueSlipId == issueSlip.Id &&
                x.IssuedUnits < x.RequestedUnits &&
                x.Position.SectionId == 2);

            Assert.AreEqual(item, issueSlip.GetFirstItemInSectionWithUnissuedUnits(2));

            Assert.AreEqual(null, issueSlip.GetFirstItemInSectionWithUnissuedUnits(0));
            Assert.AreEqual(null, issueSlip.GetFirstItemInSectionWithUnissuedUnits(1));
            Assert.AreEqual(null, issueSlip.GetFirstItemInSectionWithUnissuedUnits(3));
        }

        [TestMethod, TestCategory("Extensions")]
        public void GetUnissuedItems()
        {
            IssueSlip issueSlip = this.DatabaseContext.IssueSlips.FirstOrDefault(x => x.OrderId == 1);
            List<IssueSlip.Item> items = this.DatabaseContext.IssueSlipItems.Where(x =>
                x.IssueSlipId == issueSlip.Id &&
                x.IssuedUnits < x.RequestedUnits).ToList();

            CollectionAssert.AreEqual(items, issueSlip.GetUnissuedItems().ToList());
        }

        [TestMethod, TestCategory("Extensions")]
        public void CanBeMovedToBin()
        {
            IssueSlip issueSlip;

            issueSlip = this.DatabaseContext.IssueSlips.FirstOrDefault(x => x.OrderId == 1);
            Assert.IsFalse(issueSlip.CanBeMovedToBin());

            issueSlip = this.DatabaseContext.IssueSlips.FirstOrDefault(x => x.OrderId == 2);
            Assert.IsTrue(issueSlip.CanBeMovedToBin());
        }

        [TestMethod, TestCategory("Extensions")]
        public void CanBeRestoredFromBin()
        {
            IssueSlip issueSlip;

            issueSlip = this.DatabaseContext.IssueSlips.FirstOrDefault(x => x.OrderId == 1);
            Assert.IsFalse(issueSlip.CanBeRestoredFromBin());

            issueSlip = this.DatabaseContext.IssueSlips.FirstOrDefault(x => x.OrderId == 3);
            Assert.IsTrue(issueSlip.CanBeRestoredFromBin());
        }
    }
}
