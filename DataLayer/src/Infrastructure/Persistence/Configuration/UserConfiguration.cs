using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.FirstName)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(e => e.LastName)
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.HasOne(d => d.Location).WithMany(p => p.Users)
            .HasForeignKey(d => d.LocationId)
            .HasConstraintName("FK_User_Location");
    }
    
}