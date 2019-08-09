﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database.Configuration
{
    public class IssueSlipItemConfiguration : IEntityTypeConfiguration<IssueSlip.Item>
    {
        public void Configure(EntityTypeBuilder<IssueSlip.Item> builder)
        {
            builder.HasKey(x => new { x.IssueSlipId, x.PositionId, x.WareId });

            builder.HasOne(x => x.IssueSlip)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.IssueSlipId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Ware)
                .WithMany(x => x.IssueSlipItems)
                .HasForeignKey(x => x.WareId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Position)
                .WithMany(x => x.IssueSlipItems)
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.UtcCreated)
                .HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UtcCreated)
                .ValueGeneratedOnAdd();
        }
    }
}
