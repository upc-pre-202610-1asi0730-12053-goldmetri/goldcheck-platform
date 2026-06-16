using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyJewelryInventoryConfiguration(this ModelBuilder builder)
    {
        // ── JewelryMaterial ───────────────────────────────────────────────────

        builder.Entity<JewelryMaterial>().HasKey(m => m.Id);
        builder.Entity<JewelryMaterial>().Property(m => m.Id)
            .IsRequired().ValueGeneratedOnAdd();

        builder.Entity<JewelryMaterial>().OwnsOne(m => m.MaterialId, mid =>
        {
            mid.WithOwner().HasForeignKey("Id");
            mid.Property(v => v.Value)
                .HasColumnName("MaterialId").IsRequired().HasMaxLength(100);
        });

        builder.Entity<JewelryMaterial>().OwnsOne(m => m.JewelerId, jid =>
        {
            jid.WithOwner().HasForeignKey("Id");
            jid.Property(v => v.Value)
                .HasColumnName("JewelerId").IsRequired().HasMaxLength(100);
        });

        builder.Entity<JewelryMaterial>().OwnsOne(m => m.Status, s =>
        {
            s.WithOwner().HasForeignKey("Id");
            s.Property(v => v.Value)
                .HasColumnName("Status").IsRequired().HasMaxLength(50);
        });

        builder.Entity<JewelryMaterial>().Property(m => m.QRCodeValue)
            .HasColumnName("QRCode").HasMaxLength(200);

        builder.Entity<JewelryMaterial>().Property(m => m.CertificateIdRef)
            .HasColumnName("CertificateId").HasMaxLength(100);

        // ── Jewelry ───────────────────────────────────────────────────────────

        builder.Entity<Jewelry>().HasKey(j => j.Id);
        builder.Entity<Jewelry>().Property(j => j.Id)
            .IsRequired().ValueGeneratedOnAdd();

        builder.Entity<Jewelry>().OwnsOne(j => j.CertificateId, cid =>
        {
            cid.WithOwner().HasForeignKey("Id");
            cid.Property(v => v.Value)
                .HasColumnName("CertificateId").IsRequired().HasMaxLength(100);
        });

        builder.Entity<Jewelry>().OwnsOne(j => j.JewelerId, jid =>
        {
            jid.WithOwner().HasForeignKey("Id");
            jid.Property(v => v.Value)
                .HasColumnName("JewelerId").IsRequired().HasMaxLength(100);
        });

        builder.Entity<Jewelry>().Property(j => j.MaterialIdRef)
            .HasColumnName("MaterialId").IsRequired().HasMaxLength(100);

        builder.Entity<Jewelry>().Property(j => j.IsSigned)
            .HasColumnName("IsSigned").IsRequired();
    }
}
