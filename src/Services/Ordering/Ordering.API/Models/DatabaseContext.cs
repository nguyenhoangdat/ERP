using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }

        #region Properties
        public DbSet<InvoiceInfo> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderAction> OrderActions { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShippingInfo> Shippings { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TODO: FluentAPI for InvoiceInfo
            #region InvoiceInfo
            modelBuilder.Entity<InvoiceInfo>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<InvoiceInfo>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            #endregion

            #region Order
            modelBuilder.Entity<Order>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Order>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>()
                .HasOne(x => x.InvoiceInfo)
                .WithOne(i => i.Order)
                .HasForeignKey<Order>(x => x.InvoiceInfoId);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.ShippingInfo)
                .WithOne(i => i.Order)
                .HasForeignKey<Order>(x => x.ShippingInfoId);
            #endregion
            #region OrderAction
            modelBuilder.Entity<OrderAction>()
                .HasKey(x => new { x.OrderId, x.Status });

            modelBuilder.Entity<OrderAction>()
                .HasOne(x => x.Order)
                .WithMany(o => o.OrderActions)
                .HasForeignKey(x => x.OrderId);

            //modelBuilder.Entity<OrderAction>()
            //    .HasDefaultValue(System.DateTime.Now)
            //    .ValueGeneratedOnAdd();
            #endregion

            #region OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasKey(x => new { x.ProductId, x.OrderId });

            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(x => x.OrderId);
            #endregion

            //TODO: FluentAPI for InvoiceInfo
            #region ShippingInfo
            modelBuilder.Entity<InvoiceInfo>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<InvoiceInfo>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            #endregion
        }
    }
}
