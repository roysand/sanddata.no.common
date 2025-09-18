using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.RoleId).HasName("PK_Role");
        builder.ToTable("Role", "dbo");
        builder.Property(e => e.RoleId).ValueGeneratedNever();
        builder.Property(e => e.RoleName).HasMaxLength(50).IsUnicode(false);
        builder.Property(e =>e.RoleDescription).HasMaxLength(250).IsUnicode(false);
    }
}