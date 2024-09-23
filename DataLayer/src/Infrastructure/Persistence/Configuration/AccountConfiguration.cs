using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(e => e.AccountId).HasName("PK_Account");

        builder.ToTable("Account", "dbo");

        builder.Property(e => e.AccountId).ValueGeneratedNever();
        builder.Property(e => e.AccountName)
            .HasMaxLength(30)
            .IsUnicode(false);
        
        builder.HasOne(d => d.ApiKey).WithMany(p => p.Account)
            .HasForeignKey(d => d.ApiKeyId)
            .HasConstraintName("FK_Account_ApiKey");
    }
}