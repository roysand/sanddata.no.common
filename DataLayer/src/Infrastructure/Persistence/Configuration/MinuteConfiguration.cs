﻿using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class MinuteConfiguration : IEntityTypeConfiguration<Minute>
{
    public void Configure(EntityTypeBuilder<Minute> builder)
    {
        builder.HasKey(e => new { e.TimeStamp, e.Location }).HasName("minute_pk");

        builder.ToTable("minute");

        builder.HasIndex(e => e.TimeStamp, "IX_Minute_TimeStamp");

        builder.Property(e => e.TimeStamp).HasPrecision(3);
        builder.Property(e => e.Location)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(e => e.Unit)
            .HasMaxLength(5)
            .IsUnicode(false);
        builder.Property(e => e.ValueNum).HasColumnType("decimal(19, 5)");
    }
}