using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class AppUserLocationConfiguration : IEntityTypeConfiguration<AppUserLocation>
{
    public void Configure(EntityTypeBuilder<AppUserLocation> builder)
    {
        builder.HasKey(e => new { e.AppUserId, e.LocationId }).HasName("appuser_location_pk");

        builder.ToTable("appuser_location");

        builder.HasOne(e => e.AppUser)
            .WithMany(u => u.AppUserLocations)
            .HasForeignKey(e => e.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Location)
            .WithMany(l => l.UserLocations)
            .HasForeignKey(e => e.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}