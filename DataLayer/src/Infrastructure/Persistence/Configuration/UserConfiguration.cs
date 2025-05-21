using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.UserId);

        builder.ToTable("User");

        builder.Property(e => e.UserId).ValueGeneratedNever();
        builder.Property(e => e.Email).HasMaxLength(100);
        builder.Property(e => e.UserName).HasMaxLength(100);

        builder.HasMany(d => d.Locations).WithMany(p => p.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserLocation",
                r => r.HasOne<Location>().WithMany()
                    .HasForeignKey("LocationId")
                    .OnDelete(DeleteBehavior.ClientSetNull),
                l => l.HasOne<User>().WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.ClientSetNull),
                j =>
                {
                    j.HasKey("UserId", "LocationId");
                    j.ToTable("UserLocation");
                });
    }
}