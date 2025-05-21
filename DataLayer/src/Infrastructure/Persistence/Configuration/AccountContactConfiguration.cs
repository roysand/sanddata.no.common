using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class AccountContactConfiguration : IEntityTypeConfiguration<AccountContact>
{
    public void Configure(EntityTypeBuilder<AccountContact> builder)
    {
        builder.ToTable("AccountContact", "dbo");

        builder.Property(e => e.AccountContactId).ValueGeneratedNever();
        builder.Property(e => e.ContactEmail)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(e => e.ContactFirstName)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(e => e.ContactLastName)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(e => e.ContactMobilePhone)
            .HasMaxLength(20)
            .IsUnicode(false);

        builder.HasOne(d => d.Account).WithMany(p => p.AccountContact)
            .HasForeignKey(d => d.AccountId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_AccountContact_Account");
    }
}