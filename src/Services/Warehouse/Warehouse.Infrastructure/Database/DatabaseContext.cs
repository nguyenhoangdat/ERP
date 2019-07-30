using Microsoft.EntityFrameworkCore;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database.Configuration;

namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        protected DatabaseContext() : base()
        {
        }
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        #region DbSets
        public DbSet<IssueSlip> IssueSlips { get; set; }
        public DbSet<IssueSlip.Item> IssueSlipItems { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Receipt.Item> ReceiptItems { get; set; }
        public DbSet<Ware> Wares { get; set; }
        public DbSet<Domain.Entities.Warehouse> Warehouses { get; set; }

        public DbSet<StockTaking> StockTakings { get; set; }
        public DbSet<StockTaking.Item> StockTakingItems { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseConfiguration).Assembly);
        }
    }
}
