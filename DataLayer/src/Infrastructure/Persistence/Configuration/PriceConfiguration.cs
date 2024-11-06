using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class PriceConfiguration : IEntityTypeConfiguration<Price>
{
    public void Configure(EntityTypeBuilder<Price> builder)
    {
        builder.HasKey(e => e.PriceId)
            .HasName("price_pk")
            .IsClustered(false);

        builder.ToTable("price");

        builder.HasIndex(e => e.PriceId, "IX_Price_Id");

        builder.HasIndex(e => e.PricePeriod, "IX_Price_PricePeriod").IsUnique();

        builder.Property(e => e.PriceId).ValueGeneratedNever();
        builder.Property(e => e.Average).HasColumnType("decimal(19, 5)");
        builder.Property(e => e.Currency)
            .HasMaxLength(5)
            .IsUnicode(false);
        builder.Property(e => e.InDomain)
            .HasMaxLength(20)
            .IsUnicode(false);
        builder.Property(e => e.Location)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(e => e.Max).HasColumnType("decimal(19, 5)");
        builder.Property(e => e.Min).HasColumnType("decimal(19, 5)");
        builder.Property(e => e.ModifiedDate).HasPrecision(3);
        builder.Property(e => e.OutDomain)
            .HasMaxLength(20)
            .IsUnicode(false);
        builder.Property(e => e.PricePeriod).HasPrecision(3);
        builder.Property(e => e.Unit)
            .HasMaxLength(5)
            .IsUnicode(false);    }
}