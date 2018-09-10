using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Models
{
    public class DatabaseContext : DbContext
    {
        protected DatabaseContext() : base()
        {
        }
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        #region Properties
        public DbSet<BasketReservation> BasketReservations { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<ParametersGroup> ParametersGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<VAT> VATs { get; set; }
        public DbSet<Warranty> Warranties { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region BasketReservation
            modelBuilder.Entity<BasketReservation>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<BasketReservation>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<BasketReservation>()
                .HasOne(x => x.Product)
                .WithMany(p => p.BasketReservations)
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<BasketReservation>()
                .Property(x => x.UtcReservationTime)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<BasketReservation>()
                .Property(x => x.UtcReservationTime)
                .HasDefaultValue(DateTime.UtcNow);

            modelBuilder.Entity<BasketReservation>()
                .Property(x => x.UtcExpirationTime)
                .ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<BasketReservation>()
                .Property(x => x.UtcExpirationTime)
                .HasDefaultValue(DateTime.UtcNow.AddMinutes(10));
            #endregion
            #region Brand
            modelBuilder.Entity<Brand>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Brand>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            #endregion
            #region Category
            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Category>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //TODO: Add subcategories
            //modelBuilder.Entity<Category>()
            //    .HasOne(x => x.ParentalCategory)
            //    .WithMany(x => x.Subcategories)
            //    .HasForeignKey(x => x.ParentCategoryId);
            #endregion
            #region Files
            modelBuilder.Entity<File>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<File>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<File>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Files)
                .HasForeignKey(x => x.ProductId);
            #endregion
            #region Image
            modelBuilder.Entity<Image>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Image>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Image>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.ProductId);
            #endregion
            #region Parameter
            modelBuilder.Entity<Parameter>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Parameter>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Parameter>()
                .HasOne(x => x.Group)
                .WithMany(x => x.Parameters)
                .HasForeignKey(x => x.ParametersGroupId);
            #endregion
            #region ParametersGroup
            modelBuilder.Entity<ParametersGroup>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<ParametersGroup>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ParametersGroup>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ParametersGroups)
                .HasForeignKey(x => x.ProductId);
            #endregion
            #region Product
            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Product>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Brand)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.BrandId);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Warranty)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.WarrantyId);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.VAT)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.VATId);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Brand)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.BrandId);
            #endregion
            #region VAT
            modelBuilder.Entity<VAT>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<VAT>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            #endregion
            #region Warranty
            modelBuilder.Entity<Warranty>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Warranty>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            #endregion
        }
    }
}
