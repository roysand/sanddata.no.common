using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class PriceDetailConfiguration : IEntityTypeConfiguration<PriceDetail>
{
    public void Configure(EntityTypeBuilder<PriceDetail> builder)
    {
        builder.HasKey(e => e.PriceDetailId)
            .HasName("price_detail_pk")
            .IsClustered(false);

        builder.ToTable("price_detail");

        builder.HasIndex(e => e.PricePeriod, "IX_Price_detail_PricePeriod");

        builder.HasIndex(e => e.PriceDetailId, "IX_Price_detail_id");

        builder.Property(e => e.PriceDetailId).ValueGeneratedNever();
        builder.Property(e => e.Amount).HasColumnType("decimal(19, 5)");
        builder.Property(e => e.PricePeriod).HasPrecision(3);
    }
}