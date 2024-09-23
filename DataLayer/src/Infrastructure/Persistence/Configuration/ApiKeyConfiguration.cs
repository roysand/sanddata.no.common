using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.HasKey(e => e.ApiKeyId).HasName("PK_ApiKey");
        builder.ToTable("ApiKey");
        
        builder.Property(e => e.ApiKeyId).ValueGeneratedNever();
        builder.Property(e => e.Key)
            .HasMaxLength(40)
            .IsUnicode(false);
    }
}