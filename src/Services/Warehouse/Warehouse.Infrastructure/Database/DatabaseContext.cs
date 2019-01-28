using Microsoft.EntityFrameworkCore;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database.Configuration;

namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected DatabaseContext() : base()
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

            modelBuilder.ApplyConfiguration(new IssueSlipConfiguration());
            modelBuilder.ApplyConfiguration(new IssueSlipItemConfiguration());
            modelBuilder.ApplyConfiguration(new MovementConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new ReceiptConfiguration());
            modelBuilder.ApplyConfiguration(new ReceiptItemConfiguration());
            modelBuilder.ApplyConfiguration(new SectionConfiguration());
            modelBuilder.ApplyConfiguration(new StockTakingConfiguration());
            modelBuilder.ApplyConfiguration(new StockTakingItemConfiguration());
            modelBuilder.ApplyConfiguration(new WareConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
        }
    }
}
