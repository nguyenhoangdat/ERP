using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Tests.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Tests.Common
{
    public class DatabaseContextSeeder : IDbSeeder
    {
        public void Seed(DatabaseContext databaseContext)
        {
            #region System entities
            databaseContext.Warehouses.Add(new Domain.Entities.Warehouse(name: "System", new Address()
            {
                Street = "System",
                City = "System",
                Country = "System",
                ZipCode = "System"
            }));
            databaseContext.SaveChanges();

            databaseContext.Sections.Add(new Section(name: "System", warehouseId: 1));
            databaseContext.SaveChanges();

            databaseContext.Positions.Add(new Position(name: "System", width: 0, height: 0, depth: 0, maxWeight: 0, sectionId: 1));
            databaseContext.SaveChanges();
            #endregion

            // Create Addresses
            Address addressPrague = new Address()
            {
                Street = "Korunní 10",
                City = "Prague",
                Country = "Czech Republic",
                ZipCode = "150 90"
            };

            // Create Warehouses
            Domain.Entities.Warehouse warehousePrague = new Domain.Entities.Warehouse(name: "Warehouse - Prague", addressPrague);
            databaseContext.Warehouses.Add(warehousePrague);
            databaseContext.SaveChanges();

            // Create Sections
            Section sectionA = databaseContext.Sections.Add(new Section(name: "Section A", warehousePrague)).Entity;
            databaseContext.SaveChanges();

            Section sectionB = databaseContext.Sections.Add(new Section(name: "Section B", warehousePrague)).Entity;
            databaseContext.SaveChanges();

            // Generate Positions
            databaseContext.Positions.Add(new Position(name: "A.1", width: 1265, height: 400, depth: 400, maxWeight: 30000, sectionA));
            databaseContext.Positions.Add(new Position(name: "A.2", width: 1265, height: 400, depth: 400, maxWeight: 30000, sectionA));
            databaseContext.Positions.Add(new Position(name: "A.3", width: 1265, height: 400, depth: 400, maxWeight: 30000, sectionA));

            databaseContext.Positions.Add(new Position(name: "B.1", width: 965, height: 400, depth: 400, maxWeight: 30000, sectionB));
            databaseContext.Positions.Add(new Position(name: "B.2", width: 965, height: 400, depth: 400, maxWeight: 30000, sectionB));
            databaseContext.Positions.Add(new Position(name: "B.3", width: 965, height: 400, depth: 400, maxWeight: 30000, sectionB));
            databaseContext.SaveChanges();

            // Generate Wares
            databaseContext.Wares.Add(new Ware(productId: 13, productName: $"Samsung Galaxy J5", width: 65, height: 50, depth: 135, weight: 250));
            databaseContext.Wares.Add(new Ware(productId: 11, productName: $"Huawei Nova 3", width: 70, height: 50, depth: 150, weight: 300));
            databaseContext.Wares.Add(new Ware(productId: 12, productName: $"Huawei P20", width: 70, height: 50, depth: 150, weight: 280));
            databaseContext.SaveChanges();

            // Generate Receipts
            databaseContext.Receipts.Add(
                new Receipt(
                    utcExpected: DateTime.UtcNow,
                    utcReceived: DateTime.UtcNow,
                    items: new List<Receipt.Item>()
                    {
                        new Receipt.Item(
                            receiptId: 0,
                            positionId: 2,
                            wareId: 1,
                            countOrdered: 10,
                            countReceived: 10,
                            utcProcessed: DateTime.UtcNow),
                        new Receipt.Item(
                            receiptId: 0,
                            positionId: 3,
                            wareId: 2,
                            countOrdered: 5,
                            countReceived: 5,
                            utcProcessed: DateTime.UtcNow),
                        new Receipt.Item(
                            receiptId: 0,
                            positionId: 4,
                            wareId: 3,
                            countOrdered: 5,
                            countReceived: 5,
                            utcProcessed: DateTime.UtcNow)
                    }));
            databaseContext.Movements.Add(
                new Movement(
                    wareId: 1,
                    positionId: 2,
                    direction: Movement.Direction.In,
                    countChange: 10,
                    countTotal: 10));
            databaseContext.Movements.Add(
                new Movement(
                    wareId: 2,
                    positionId: 3,
                    direction: Movement.Direction.In,
                    countChange: 5,
                    countTotal: 5));
            databaseContext.Movements.Add(
                new Movement(
                    wareId: 3,
                    positionId: 4,
                    direction: Movement.Direction.In,
                    countChange: 5,
                    countTotal: 5));
            databaseContext.SaveChanges();

            databaseContext.Receipts.Add(new Receipt(
                    utcExpected: DateTime.UtcNow.AddHours(5),
                    items: new List<Receipt.Item>()
                    {
                        new Receipt.Item(1, 10)
                    }));
            databaseContext.SaveChanges();

            databaseContext.Receipts.Add(
                new Receipt(
                    utcExpected: DateTime.UtcNow.AddDays(1),
                    items: new List<Receipt.Item>()
                    {
                        new Receipt.Item(2, 5)
                    }));
            databaseContext.SaveChanges();

            // Generate IssueSlips
            databaseContext.IssueSlips.Add(
                new IssueSlip(
                    orderId: 1,
                    utcDispatchDate: DateTime.UtcNow.AddHours(1),
                    utcDeliveryDate: DateTime.UtcNow.AddHours(6),
                    new List<IssueSlip.Item>()
                    {
                        new IssueSlip.Item(
                            issueSlipId: 0,
                            wareId: 1,
                            positionId: 2,
                            requestedUnits: 1,
                            issuedUnits: 1),
                        new IssueSlip.Item(
                            issueSlipId: 0,
                            wareId: 2,
                            positionId: 1, // Unassigned position
                            requestedUnits: 1,
                            issuedUnits: 0), // Unissued item
                        new IssueSlip.Item(
                            issueSlipId: 0,
                            wareId: 3,
                            positionId: 4,
                            requestedUnits: 1,
                            issuedUnits: 0) // Unissued item
                    }));
            databaseContext.Movements.Add(
                new Movement(
                    wareId: 1,
                    positionId: 2,
                    direction: Movement.Direction.Out,
                    countChange: 1,
                    countTotal: 9));
            databaseContext.IssueSlips.Add(
                new IssueSlip(
                    orderId: 2,
                    utcDispatchDate: DateTime.UtcNow.AddHours(2),
                    utcDeliveryDate: DateTime.UtcNow.AddHours(7),
                    new List<IssueSlip.Item>()
                    ));
            databaseContext.IssueSlips.Add(
                new IssueSlip(
                    orderId: 3,
                    utcDispatchDate: DateTime.UtcNow.AddHours(2),
                    utcDeliveryDate: DateTime.UtcNow.AddHours(7),
                    new List<IssueSlip.Item>()
                    {
                        new IssueSlip.Item(
                            issueSlipId: 0,
                            wareId: 1,
                            positionId: 2,
                            requestedUnits: 1,
                            issuedUnits: 1) { UtcMovedToBin = DateTime.UtcNow }
                    }
                ) { UtcMovedToBin = DateTime.UtcNow });
            databaseContext.SaveChanges();

            // Generate StockTakings (including empty positions)
            databaseContext.StockTakings.Add(
                new StockTaking(
                    name: "StockTaking for Warehouse - Praha",
                    items: new List<StockTaking.Item>()
                    {
                        new StockTaking.Item(stockTakingId: 0, wareId: 1, positionId: 2, currentStock: 9, countedStock: 9, utcCounted: DateTime.UtcNow.AddSeconds(30)),
                        new StockTaking.Item(stockTakingId: 0, wareId: null, positionId: 3, currentStock: 0, countedStock: 0, utcCounted: DateTime.UtcNow.AddSeconds(35)),
                        new StockTaking.Item(stockTakingId: 0, wareId: null, positionId: 4, currentStock: 0, countedStock: 0, utcCounted: DateTime.UtcNow.AddSeconds(40)),

                        new StockTaking.Item(stockTakingId: 0, wareId: null, positionId: 5, currentStock: 0, countedStock: 0, utcCounted: DateTime.UtcNow.AddSeconds(45)),
                        new StockTaking.Item(stockTakingId: 0, wareId: null, positionId: 6, currentStock: 0, countedStock: 0, utcCounted: DateTime.UtcNow.AddSeconds(50)),
                        new StockTaking.Item(stockTakingId: 0, wareId: null, positionId: 7, currentStock: 0, countedStock: 0, utcCounted: DateTime.UtcNow.AddSeconds(55))
                    }));
            databaseContext.SaveChanges();

            // TODO: Do revision for "GetNextToBeProcessed" for Receipt, IssueSlip, and StockTaking - do we need the parentId?
        }
    }
}
