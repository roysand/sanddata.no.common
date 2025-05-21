using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Infrastructure.Persistence.Configuration;

public class DetailConfiguration : IEntityTypeConfiguration<Detail>
{
    public void Configure(EntityTypeBuilder<Detail> builder)
    {
        builder.HasKey(e => e.Id).HasName("detail_pk");

        builder.ToTable("detail");

        builder.HasIndex(e => e.TimeStamp, "IX_Detail_TimeStamp");

        builder.HasIndex(e => new { e.ObisCodeId, e.TimeStamp }, "nci_wi_detail_99F2D155AA826D3C5CC127D4720BFE87");

        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.Name)
            .HasMaxLength(30)
            .IsUnicode(false);
        builder.Property(e => e.ObisCode)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(e => e.TimeStamp).HasPrecision(3);
        builder.Property(e => e.Unit)
            .HasMaxLength(5)
            .IsUnicode(false);
        builder.Property(e => e.ValueNum).HasColumnType("decimal(19, 5)");
        builder.Property(e => e.ValueStr)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.HasOne(e => e.Location)
            .WithMany()
            .HasForeignKey(e => e.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}