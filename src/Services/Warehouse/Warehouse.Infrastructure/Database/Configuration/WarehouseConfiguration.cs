using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database.Configuration
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Domain.Entities.Warehouse>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Warehouse> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(w => w.Address)
                .WithOne(a => a.Warehouse)
                .HasForeignKey<Domain.Entities.Warehouse>(w => w.AddressId);

            builder.Property(x => x.UtcCreated)
                .HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UtcCreated)
                .ValueGeneratedOnAdd();
        }
    }
}
