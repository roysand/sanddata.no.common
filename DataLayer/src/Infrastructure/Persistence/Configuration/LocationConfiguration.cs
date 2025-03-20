using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("Location");

        builder.Property(e => e.LocationId).ValueGeneratedNever();
        builder.Property(e => e.LocationAddress)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(e => e.LocationName)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(e => e.SerialNumber)
            .HasMaxLength(20)
            .IsUnicode(false);
    }
}