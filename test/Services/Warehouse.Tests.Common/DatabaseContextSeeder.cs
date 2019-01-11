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
        public DatabaseContextSeeder(DatabaseContext context)
        {
            this.DatabaseContext = context;
            this.DatabaseContext.Database.EnsureCreated();
        }

        protected DatabaseContext DatabaseContext { get; }

        public void Seed()
        {
            // Create addresses
            Address addressPrague = new Address()
            {
                Street = "Korunní 10",
                City = "Prague",
                Country = "Czech Republic",
                ZipCode = "150 90"
            };
            Address addressBrno = new Address()
            {
                Street = "Příčná 34",
                City = "Brno",
                Country = "Czech Republic",
                ZipCode = "298 90"
            };

            // Create warehouses
            Domain.Entities.Warehouse warehousePrague = new Domain.Entities.Warehouse("Warehouse in Prague", addressPrague);
            Domain.Entities.Warehouse warehouseBrno = new Domain.Entities.Warehouse("Warehouse in Brno", addressBrno);
            this.DatabaseContext.Warehouses.Add(warehousePrague);
            this.DatabaseContext.Warehouses.Add(warehouseBrno);

            // Create sections
            Section warehousePragueSection = new Section("Section Prague 1", warehousePrague);
            Section warehouseBrnoSection = new Section("Section Brno 1", warehouseBrno);
            this.DatabaseContext.Sections.Add(warehousePragueSection);
            this.DatabaseContext.Sections.Add(warehouseBrnoSection);

            // Conts for the dimension of the positions
            const double Width = 10;
            const double Height = 10;
            const double Depth = 10;
            const double MaxWeight = 10;

            // Const for wares generation
            const int NumberOfWares = 10;

            // Generate positions
            for (int i = 1; i <= NumberOfWares; i++)
            {
                this.DatabaseContext.Positions.Add(new Position($"Position Prague {i}", Width, Height, Depth, MaxWeight, warehousePragueSection, i));
            }
            for (int i = 1; i <= NumberOfWares * 2; i++)
            {
                this.DatabaseContext.Positions.Add(new Position($"Position Brno {i}", Width, Height, Depth, MaxWeight, warehouseBrnoSection, i));
            }

            // Generate wares
            for (int i = 1; i <= NumberOfWares * 5; i++)
            {
                this.DatabaseContext.Wares.Add(new Ware(0, $"Ware {i}", Convert.ToDouble(i), Convert.ToDouble(i), Convert.ToDouble(i), Convert.ToDouble(i)));
            }

            List<Ware> wares = this.DatabaseContext.Wares.Take(NumberOfWares).ToList();
            List<Position> positions = this.DatabaseContext.Positions.OrderBy(x => x.Rating).ToList();

            /* Generate movements and IssueSlip:
             *      Assign each ware to two positions and generate movement => every ware will have two positions
             *      First position:  12 units
             *      Second position: 22 units
             *      
             *      Each IssueSlip contans one Item with Issued = 2 and Requested units = 2
             */
            for (int w = 0; w < wares.Count(); w++)
            {
                Ware ware = wares[w];

                for (int p = w * 2; p < w * 2 + 2; p++)
                {
                    Position position = positions[p];

                    // Generate movements
                    int initialValue = 10 * (w % 2 + 1); // Produce 10 or 20
                    int movedUnits = 2; // Requested and Issued units

                    this.DatabaseContext.Movements.Add(new Movement(ware, position, Movement.Direction.In, Movement.EntryContent.Delivery, initialValue, initialValue));
                    this.DatabaseContext.Movements.Add(new Movement(ware, position, Movement.Direction.Out, Movement.EntryContent.Receipt, movedUnits, initialValue - movedUnits));
                    this.DatabaseContext.Movements.Add(new Movement(ware, position, Movement.Direction.In, Movement.EntryContent.Delivery, movedUnits * 2, initialValue + movedUnits));

                    // Generate IssueSlip
                    StockTaking issueSlip = new StockTaking($"{ware.ToString()} {position.ToString()}", DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);
                    issueSlip.Items.Add(new StockTaking.Item(issueSlip, ware, position, movedUnits, movedUnits));
                    this.DatabaseContext.IssueSlips.Add(issueSlip);
                }
            }
        }
    }
}
