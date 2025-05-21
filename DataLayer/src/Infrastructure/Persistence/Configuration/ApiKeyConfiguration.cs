using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.HasKey(e => e.ApiKeyId);

        builder.ToTable("Apikey");

        builder.HasIndex(e => e.UserId).IsUnique();

        builder.Property(e => e.ApiKeyId).ValueGeneratedNever();
        builder.Property(e => e.Key).HasMaxLength(200);

        builder.HasOne(d => d.User).WithOne(p => p.ApiKey)
            .HasForeignKey<ApiKey>(d => d.UserId);
    }
}