using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database.Configuration
{
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(s => s.Warehouse)
                .WithMany(w => w.Sections)
                .HasForeignKey(s => s.WarehouseId);

            builder.Property(x => x.UtcCreated)
                .HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UtcCreated)
                .ValueGeneratedOnAdd();
        }
    }
}
