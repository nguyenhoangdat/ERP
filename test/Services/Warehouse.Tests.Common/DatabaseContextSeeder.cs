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
            if (this.SeedSystemEntities(databaseContext) != 3)
            {
                throw new Exception("System entities seed failed!");
            }

            // Create Addresses
            Address addressPrague = new Address()
            {
                Street = "Korunní 10",
                City = "Prague",
                Country = "Czech Republic",
                ZipCode = "150 90"
            };

            // Create Warehouses
            Domain.Entities.Warehouse warehousePrague = new Domain.Entities.Warehouse(name: "Warehouse - Prague", addressPrague)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1)
            };
            databaseContext.Warehouses.Add(warehousePrague);
            databaseContext.SaveChanges();

            // Create Sections
            Section sectionA = databaseContext.Sections.Add(new Section(name: "Section A", warehousePrague)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1)
            }).Entity;
            databaseContext.SaveChanges();

            Section sectionB = databaseContext.Sections.Add(new Section(name: "Section B", warehousePrague)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1)
            }).Entity;
            databaseContext.SaveChanges();

            // Generate Positions
            databaseContext.Positions.Add(new Position(name: "A.01", width: 1265, height: 400, depth: 400, maxWeight: 30000, sectionA)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1)
            });
            databaseContext.Positions.Add(new Position(name: "A.02", width: 1265, height: 400, depth: 400, maxWeight: 30000, sectionA)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1)
            });
            databaseContext.Positions.Add(new Position(name: "A.03", width: 1265, height: 400, depth: 400, maxWeight: 30000, sectionA)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1)
            });
            databaseContext.Positions.Add(new Position(name: "A.04", width: 1265, height: 400, depth: 400, maxWeight: 30000, sectionA)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1)
            });
            databaseContext.SaveChanges();

            databaseContext.Positions.Add(new Position(name: "B.01", width: 965, height: 400, depth: 400, maxWeight: 30000, sectionB)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1)
            });
            databaseContext.Positions.Add(new Position(name: "B.02", width: 965, height: 400, depth: 400, maxWeight: 30000, sectionB)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1)
            });
            databaseContext.Positions.Add(new Position(name: "B.03", width: 965, height: 400, depth: 400, maxWeight: 30000, sectionB)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1)
            });
            databaseContext.SaveChanges();

            // Generate Wares
            databaseContext.Wares.Add(new Ware(productId: 13, productName: $"Samsung Galaxy J5", width: 65, height: 50, depth: 135, weight: 250)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1).AddHours(1)
            });
            databaseContext.Wares.Add(new Ware(productId: 11, productName: $"Huawei Nova 3", width: 70, height: 50, depth: 150, weight: 300)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1).AddHours(1)
            });
            databaseContext.Wares.Add(new Ware(productId: 12, productName: $"Huawei P20", width: 70, height: 50, depth: 150, weight: 280)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1).AddHours(1)
            });
            databaseContext.Wares.Add(new Ware(productId: 12, productName: $"Samsung Galaxy A5", width: 70, height: 50, depth: 150, weight: 280)
            {
                UtcCreated = DateTime.UtcNow.AddMonths(-1).AddHours(1)
            });
            databaseContext.SaveChanges();

            #region Receipts
            // Generate Receipts
            databaseContext.Receipts.Add(
                new Receipt(
                    utcExpected: DateTime.UtcNow.AddMonths(-1).AddDays(1).AddHours(6),
                    utcReceived: DateTime.UtcNow.AddMonths(-1).AddDays(1).AddHours(7),
                    items: new List<Receipt.Item>()
                    {
                        new Receipt.Item(
                            receiptId: 0,
                            positionId: 2,
                            wareId: 1,
                            countOrdered: 10,
                            countReceived: 10,
                            utcProcessed: DateTime.UtcNow.AddMonths(-1).AddDays(1).AddHours(10).AddMinutes(1))
                        {
                            UtcCreated = DateTime.UtcNow.AddMonths(-1).AddDays(1)
                        },
                        new Receipt.Item(
                            receiptId: 0,
                            positionId: 3,
                            wareId: 2,
                            countOrdered: 5,
                            countReceived: 5,
                            utcProcessed: DateTime.UtcNow.AddMonths(-1).AddDays(1).AddHours(10).AddMinutes(2))
                        {
                            UtcCreated = DateTime.UtcNow.AddMonths(-1).AddDays(1)
                        },
                        new Receipt.Item(
                            receiptId: 0,
                            positionId: 4,
                            wareId: 3,
                            countOrdered: 5,
                            countReceived: 5,
                            utcProcessed: DateTime.UtcNow.AddMonths(-1).AddDays(1).AddHours(10).AddMinutes(3))
                        {
                            UtcCreated = DateTime.UtcNow.AddMonths(-1).AddDays(1)
                        },
                        new Receipt.Item(
                            receiptId: 0,
                            positionId: 5,
                            wareId: 4,
                            countOrdered: 1,
                            countReceived: 1,
                            utcProcessed: DateTime.UtcNow.AddMonths(-1).AddDays(1).AddHours(10).AddMinutes(4))
                        {
                            UtcCreated = DateTime.UtcNow.AddMonths(-1).AddDays(1)
                        }
                    })
                {
                    UtcCreated = DateTime.UtcNow.AddMonths(-1).AddDays(1)
                });
            databaseContext.Movements.Add(
                new Movement(
                    wareId: 1,
                    positionId: 2,
                    direction: Movement.Direction.In,
                    countChange: 10,
                    countTotal: 10)
                {
                    UtcCreated = DateTime.UtcNow.AddMonths(-1).AddDays(1).AddHours(10).AddMinutes(11)
                });
            databaseContext.Movements.Add(
                new Movement(
                    wareId: 2,
                    positionId: 3,
                    direction: Movement.Direction.In,
                    countChange: 5,
                    countTotal: 5)
                {
                    UtcCreated = DateTime.UtcNow.AddMonths(-1).AddDays(1).AddHours(10).AddMinutes(12)
                });
            databaseContext.Movements.Add(
                new Movement(
                    wareId: 3,
                    positionId: 4,
                    direction: Movement.Direction.In,
                    countChange: 5,
                    countTotal: 5)
                {
                    UtcCreated = DateTime.UtcNow.AddMonths(-1).AddDays(1).AddHours(10).AddMinutes(13)
                });
            databaseContext.Movements.Add(
                new Movement(
                    wareId: 4,
                    positionId: 5,
                    direction: Movement.Direction.In,
                    countChange: 1,
                    countTotal: 1)
                {
                    UtcCreated = DateTime.UtcNow.AddMonths(-1).AddDays(1).AddHours(10).AddMinutes(14)
                });
            databaseContext.SaveChanges();

            // Future Receipts
            databaseContext.Receipts.Add(
                new Receipt(
                    utcExpected: DateTime.UtcNow.AddDays(2).AddHours(5),
                    items: new List<Receipt.Item>()
                    {
                        new Receipt.Item(0, 2, 1, 10, 0)
                        {
                            UtcCreated = DateTime.UtcNow.AddDays(-2)
                        }
                    })
                {
                    UtcCreated = DateTime.UtcNow.AddDays(-2)
                });
            databaseContext.SaveChanges();

            databaseContext.Receipts.Add(
                new Receipt(
                    utcExpected: DateTime.UtcNow.AddDays(3).AddHours(4),
                    items: new List<Receipt.Item>()
                    {
                        new Receipt.Item(2, 5)
                        {
                            UtcCreated = DateTime.UtcNow.AddDays(-1)
                        }
                    })
                {
                    UtcCreated = DateTime.UtcNow.AddDays(-1)
                });
            databaseContext.SaveChanges();
            #endregion

            #region IssueSlips
            // Generate IssueSlips - Past
            databaseContext.IssueSlips.Add(
                new IssueSlip(
                    orderId: 1,
                    utcDispatchDate: DateTime.UtcNow.AddDays(-15),
                    utcDeliveryDate: DateTime.UtcNow.AddDays(-14),
                    new List<IssueSlip.Item>()
                    {
                        new IssueSlip.Item(
                            issueSlipId: 0,
                            wareId: 1,
                            positionId: 2,
                            requestedUnits: 1,
                            issuedUnits: 1)
                        {
                            UtcCreated = DateTime.UtcNow.AddDays(-16)
                        },
                        new IssueSlip.Item(
                            issueSlipId: 0,
                            wareId: 4,
                            positionId: 5,
                            requestedUnits: 1,
                            issuedUnits: 1)
                        {
                            UtcCreated = DateTime.UtcNow.AddDays(-16)
                        }
                    })
                {
                    UtcCreated = DateTime.UtcNow.AddDays(-16)
                });
            databaseContext.Movements.Add(
                new Movement(
                    wareId: 1,
                    positionId: 2,
                    direction: Movement.Direction.Out,
                    countChange: 1,
                    countTotal: 9)
                {
                    UtcCreated = DateTime.UtcNow.AddDays(-16).AddHours(12)
                });
            databaseContext.Movements.Add(
               new Movement(
                   wareId: 4,
                   positionId: 5,
                   direction: Movement.Direction.Out,
                   countChange: 1,
                   countTotal: 0)
               {
                   UtcCreated = DateTime.UtcNow.AddDays(-16).AddHours(12).AddMinutes(2)
               });

            // IssueSlips - Current
            databaseContext.IssueSlips.Add(
                new IssueSlip(
                    orderId: 2,
                    utcDispatchDate: DateTime.UtcNow.AddDays(1),
                    utcDeliveryDate: DateTime.UtcNow.AddDays(1).AddHours(12),
                    new List<IssueSlip.Item>()
                    {
                        new IssueSlip.Item(
                            issueSlipId: 0,
                            wareId: 2,
                            positionId: 1, // Unassigned position
                            requestedUnits: 1,
                            issuedUnits: 0) // Unissued item
                        {
                            UtcCreated = DateTime.UtcNow.AddDays(-1)
                        }, 
                        new IssueSlip.Item(
                            issueSlipId: 0,
                            wareId: 3,
                            positionId: 4,
                            requestedUnits: 1,
                            issuedUnits: 0) // Unissued item
                        {
                            UtcCreated = DateTime.UtcNow.AddDays(-1)
                        },
                        new IssueSlip.Item(
                            issueSlipId: 0,
                            wareId: 3,
                            positionId: 1,
                            requestedUnits: 1,
                            issuedUnits: 0) // Unissued item
                        {
                            UtcCreated = DateTime.UtcNow.AddDays(-1),
                            UtcMovedToBin = DateTime.UtcNow.AddDays(-1)
                        }
                    })
                {
                    UtcCreated = DateTime.UtcNow.AddDays(-1)
                });
            databaseContext.Positions.FirstOrDefault(x => x.Id == 4).ReservedUnits += 1;
            databaseContext.SaveChanges();

            // IssueSlip - Current - Cancelled order
            databaseContext.IssueSlips.Add(
                new IssueSlip(
                    orderId: 3,
                    utcDispatchDate: DateTime.UtcNow.AddDays(1),
                    utcDeliveryDate: DateTime.UtcNow.AddDays(1).AddHours(12),
                    new List<IssueSlip.Item>()
                    {
                        new IssueSlip.Item(
                            issueSlipId: 0,
                            wareId: 2,
                            positionId: 1, // Unassigned position
                            requestedUnits: 1,
                            issuedUnits: 0) // Unissued
                        {
                            UtcCreated = DateTime.UtcNow.AddDays(-1),
                            UtcMovedToBin = DateTime.UtcNow.AddDays(-1).AddHours(1)
                        }
                    })
                {
                    UtcCreated = DateTime.UtcNow.AddDays(-1),
                    UtcMovedToBin = DateTime.UtcNow.AddDays(-1).AddHours(1)
                });
            databaseContext.SaveChanges();
            #endregion

            // Generate StockTakings (including empty positions)
            databaseContext.StockTakings.Add(
                new StockTaking(
                    name: "StockTaking for Warehouse - Praha",
                    items: new List<StockTaking.Item>()
                    {
                        new StockTaking.Item(stockTakingId: 0, wareId: 1, positionId: 2, currentStock: 9, countedStock: 9, utcCounted: DateTime.UtcNow.AddSeconds(30)),
                        new StockTaking.Item(stockTakingId: 0, wareId: 2, positionId: 3, currentStock: 5, countedStock: 0, utcCounted: DateTime.UtcNow.AddSeconds(35)),
                        new StockTaking.Item(stockTakingId: 0, wareId: 3, positionId: 4, currentStock: 5, countedStock: 0, utcCounted: DateTime.UtcNow.AddSeconds(40)),
                        new StockTaking.Item(stockTakingId: 0, wareId: 4, positionId: 5, currentStock: 0, countedStock: 0),

                        new StockTaking.Item(stockTakingId: 0, wareId: null, positionId: 6, currentStock: 0, countedStock: 0, utcCounted: DateTime.UtcNow.AddSeconds(45)),
                        new StockTaking.Item(stockTakingId: 0, wareId: null, positionId: 7, currentStock: 0, countedStock: 0, utcCounted: DateTime.UtcNow.AddSeconds(50)),
                        new StockTaking.Item(stockTakingId: 0, wareId: null, positionId: 8, currentStock: 0, countedStock: 0, utcCounted: DateTime.UtcNow.AddSeconds(55))
                    }));
            databaseContext.SaveChanges();

            // TODO: Do revision for "GetNextToBeProcessed" for Receipt, IssueSlip, and StockTaking - do we need the parentId?
        }

        private int SeedSystemEntities(DatabaseContext databaseContext)
        {
            int count = 0;

            databaseContext.Warehouses.Add(new Domain.Entities.Warehouse(name: "System", new Address()
            {
                Street = "System",
                City = "System",
                Country = "System",
                ZipCode = "System"
            }));
            count += databaseContext.SaveChanges();

            databaseContext.Sections.Add(new Section(name: "System", warehouseId: 1));
            count += databaseContext.SaveChanges();

            databaseContext.Positions.Add(new Position(name: "System", width: 0, height: 0, depth: 0, maxWeight: 0, sectionId: 1));
            count += databaseContext.SaveChanges();

            return count;
        }
    }
}
