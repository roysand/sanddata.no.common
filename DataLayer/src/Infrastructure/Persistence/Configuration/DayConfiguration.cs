using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class DayConfiguration : IEntityTypeConfiguration<Day>
{
    public void Configure(EntityTypeBuilder<Day> builder)
    {
        builder.HasKey(e => new { e.Date, e.Location }).HasName("day_pk");

        builder.ToTable("day");

        builder.HasIndex(e => e.Date, "IX_Day_Date");

        builder.Property(e => e.Date).HasPrecision(3);
        builder.Property(e => e.Location)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(e => e.PriceNok)
            .HasColumnType("decimal(19, 5)")
            .HasColumnName("PriceNOK");
        builder.Property(e => e.Unit)
            .HasMaxLength(5)
            .IsUnicode(false);
        builder.Property(e => e.ValueNum).HasColumnType("decimal(19, 5)");
    }
}