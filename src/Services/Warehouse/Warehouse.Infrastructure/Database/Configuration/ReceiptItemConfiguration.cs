using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database.Configuration
{
    public class ReceiptItemConfiguration : IEntityTypeConfiguration<Receipt.Item>
    {
        public void Configure(EntityTypeBuilder<Receipt.Item> builder)
        {
            builder.HasKey(x => new { x.ReceiptId, x.WareId });

            builder.HasOne(x => x.Receipt)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.ReceiptId);

            builder.HasOne(x => x.Ware)
                .WithMany(x => x.ReceiptItems)
                .HasForeignKey(x => x.WareId);

            builder.HasOne(x => x.Position)
                .WithMany(x => x.ReceiptItems)
                .HasForeignKey(x => x.PositionId);

            builder.Property(x => x.UtcCreated)
                .HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UtcCreated)
                .ValueGeneratedOnAdd();
        }
    }
}
