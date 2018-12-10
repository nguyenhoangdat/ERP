using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database.Configuration
{
    public class StockTakingItemConfiguration : IEntityTypeConfiguration<StockTaking.Item>
    {
        public void Configure(EntityTypeBuilder<StockTaking.Item> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(i => i.StockTaking)
                .WithMany(s => s.Items)
                .HasForeignKey(i => i.StockTakingId);

            builder.HasOne(i => i.Ware)
                .WithMany(s => s.StockTakingItems)
                .HasForeignKey(i => i.WareId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Position)
                .WithMany(p => p.StockTakingItems)
                .HasForeignKey(i => i.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.UtcCreated)
                .HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UtcCreated)
                .ValueGeneratedOnAdd();
        }
    }
}
