using Microsoft.EntityFrameworkCore;

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
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Ware> Wares { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<StockTaking> StockTakings { get; set; }
        public DbSet<StockTakingItem> StockTakingItems { get; set; }
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
                .Property(m => m.DateCreated)
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
            #endregion
            #region Ware
            modelBuilder.Entity<Ware>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Ware>()
                .Property(x => x.Id)
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
            #endregion
        }
    }
}
