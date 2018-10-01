using Microsoft.EntityFrameworkCore;
using System;

namespace Warehouse.API.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected DatabaseContext() : base()
        {
        }

        #region Properties
        public DbSet<Address> Addresses { get; set; }
        public DbSet<IssueSlip> IssueSlips { get; set; }
        public DbSet<IssueSlip.Item> IssueSlipItems { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Ware> Wares { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<StockTaking> StockTakings { get; set; }
        public DbSet<StockTaking.Item> StockTakingItems { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Address
            modelBuilder.Entity<Address>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Address>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Address>()
                .Property(x => x.UtcCreatedAt)
                .HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<Address>()
                .Property(x => x.UtcCreatedAt)
                .ValueGeneratedOnAdd();
            #endregion
            #region IssueSlip
            modelBuilder.Entity<IssueSlip>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<IssueSlip>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<IssueSlip>()
                .Property(x => x.UtcCreatedAt)
                .HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<IssueSlip>()
                .Property(x => x.UtcCreatedAt)
                .ValueGeneratedOnAdd();
            #endregion
            #region IssueSlip.Item
            modelBuilder.Entity<IssueSlip.Item>()
                .HasKey(x => new { x.IssueSlipId, x.WareId });

            modelBuilder.Entity<IssueSlip.Item>()
                .HasOne(x => x.IssueSlip)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.IssueSlipId);

            modelBuilder.Entity<IssueSlip.Item>()
                .HasOne(x => x.Ware)
                .WithMany(x => x.IssueSlipItems)
                .HasForeignKey(x => x.WareId);

            modelBuilder.Entity<IssueSlip.Item>()
                .Property(x => x.UtcCreatedAt)
                .HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<IssueSlip.Item>()
                .Property(x => x.UtcCreatedAt)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<IssueSlip.Item>()
                .HasOne(x => x.Position)
                .WithMany(x => x.IssueSlipItems)
                .HasForeignKey(x => x.PositionId);
            #endregion
            #region Movement
            modelBuilder.Entity<Movement>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Movement>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Movement>()
                .HasOne(m => m.Ware)
                .WithMany(s => s.Movements)
                .HasForeignKey(m => m.WareId);

            modelBuilder.Entity<Movement>()
                .HasOne(m => m.Position)
                .WithMany(p => p.Movements)
                .HasForeignKey(m => m.PositionId);

            modelBuilder.Entity<Movement>()
                .Property(x => x.UtcCreatedAt)
                .HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<Movement>()
                .Property(x => x.UtcCreatedAt)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Movement>()
                .Property(m => m.UtcCreatedAt)
                .ValueGeneratedOnAdd();
            #endregion
            #region Position
            modelBuilder.Entity<Position>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Position>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Position>()
                .HasOne(p => p.Section)
                .WithMany(s => s.Positions)
                .HasForeignKey(p => p.SectionId);

            modelBuilder.Entity<Position>()
                .HasOne(p => p.Ware)
                .WithMany(s => s.Positions)
                .HasForeignKey(p => p.WareId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Position>()
                .Property(x => x.UtcCreatedAt)
                .HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<Position>()
                .Property(x => x.UtcCreatedAt)
                .ValueGeneratedOnAdd();
            #endregion
            #region Section
            modelBuilder.Entity<Section>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Section>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Section>()
                .HasOne(s => s.Warehouse)
                .WithMany(w => w.Sections)
                .HasForeignKey(s => s.WarehouseId);

            modelBuilder.Entity<Section>()
                .Property(x => x.UtcCreatedAt)
                .HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<Section>()
                .Property(x => x.UtcCreatedAt)
                .ValueGeneratedOnAdd();
            #endregion
            #region StockTaking
            modelBuilder.Entity<StockTaking>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<StockTaking>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StockTaking>()
                .Property(x => x.DateTime)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StockTaking>()
                .HasOne(st => st.Warehouse)
                .WithMany(w => w.StockTakings)
                .HasForeignKey(st => st.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockTaking>()
                .Property(x => x.UtcCreatedAt)
                .HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<StockTaking>()
                .Property(x => x.UtcCreatedAt)
                .ValueGeneratedOnAdd();
            #endregion
            #region StockTakingItem
            modelBuilder.Entity<StockTaking.Item>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<StockTaking.Item>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StockTaking.Item>()
                .HasOne(i => i.StockTaking)
                .WithMany(s => s.StockTakingItems)
                .HasForeignKey(i => i.StockTakingId);

            modelBuilder.Entity<StockTaking.Item>()
                .HasOne(i => i.Ware)
                .WithMany(s => s.StockTakingItems)
                .HasForeignKey(i => i.WareId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockTaking.Item>()
                .HasOne(i => i.Position)
                .WithMany(p => p.Items)
                .HasForeignKey(i => i.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockTaking.Item>()
                .Property(x => x.UtcCreatedAt)
                .HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<StockTaking.Item>()
                .Property(x => x.UtcCreatedAt)
                .ValueGeneratedOnAdd();
            #endregion
            #region Ware
            modelBuilder.Entity<Ware>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Ware>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Ware>()
                .Property(x => x.UtcCreatedAt)
                .HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<Ware>()
                .Property(x => x.UtcCreatedAt)
                .ValueGeneratedOnAdd();
            #endregion
            #region Warehouse
            modelBuilder.Entity<Warehouse>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Warehouse>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Warehouse>()
                .HasOne(w => w.Address)
                .WithOne(a => a.Warehouse)
                .HasForeignKey<Warehouse>(w => w.AddressId);

            modelBuilder.Entity<Warehouse>()
                .Property(x => x.UtcCreatedAt)
                .HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<Warehouse>()
                .Property(x => x.UtcCreatedAt)
                .ValueGeneratedOnAdd();
            #endregion            
        }
    }
}
