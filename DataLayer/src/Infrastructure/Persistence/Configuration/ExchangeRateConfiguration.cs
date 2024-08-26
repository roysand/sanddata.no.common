using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasKey(e => e.ExchangeRateId)
            .HasName("exchange_rate_pk")
            .IsClustered(false);

        builder.ToTable("exchange_rate");

        builder.HasIndex(e => e.ExchangeRatePeriod, "IX_exchange_rate_exchangerateperiod");

        builder.HasIndex(e => e.ExchangeRateId, "uk_exchange_rate").IsUnique();

        builder.Property(e => e.ExchangeRateId).ValueGeneratedNever();
        builder.Property(e => e.ExchangeRate1)
            .HasColumnType("decimal(19, 5)")
            .HasColumnName("ExchangeRate");
        builder.Property(e => e.ExchangeRatePeriod).HasPrecision(3);
    }
}