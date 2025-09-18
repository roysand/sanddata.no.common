using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        builder.HasKey(e => new { e.AppUserId, e.RoleId }).HasName("appuser_role_pk");
        builder.ToTable("appuser_role", "dbo");
        
        builder.HasOne(e => e.AppUser)
            .WithMany(a => a.AppUserRoles)
            .HasForeignKey(e => e.AppUserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_appuser_role");
        
        builder.HasOne(e => e.Role)
            .WithMany(r => r.AppUserRoles)
            .HasForeignKey(e => e.RoleId)
            .HasConstraintName("FK_role_appuser")
            .OnDelete(DeleteBehavior.Cascade);
    }
}