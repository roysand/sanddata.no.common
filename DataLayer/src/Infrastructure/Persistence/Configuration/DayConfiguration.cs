﻿using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class DayConfiguration : IEntityTypeConfiguration<Day>
{
    public void Configure(EntityTypeBuilder<Day> builder)
    {
        builder.HasKey(e => new { e.Date, e.LocationId }).HasName("day_pk");

        builder.ToTable("day");

        builder.HasIndex(e => e.Date, "IX_Day_Date");

        builder.Property(e => e.Date).HasPrecision(3);
        builder.Property(e => e.PriceNok)
            .HasColumnType("decimal(19, 5)")
            .HasColumnName("PriceNOK");
        builder.Property(e => e.Unit)
            .HasMaxLength(5)
            .IsUnicode(false);
        builder.Property(e => e.ValueNum).HasColumnType("decimal(19, 5)");
        
        builder.HasOne(e => e.Location)
            .WithMany(d => d.Days)
            .HasForeignKey(e => e.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}