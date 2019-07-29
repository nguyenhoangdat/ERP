using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database.Configuration
{
    public class MovementConfiguration : IEntityTypeConfiguration<Movement>
    {
        public void Configure(EntityTypeBuilder<Movement> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(m => m.Ware)
                .WithMany(s => s.Movements)
                .HasForeignKey(m => m.WareId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Position)
                .WithMany(p => p.Movements)
                .HasForeignKey(m => m.PositionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.UtcCreated)
                .HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UtcCreated)
                .ValueGeneratedOnAdd();
        }
    }
}
