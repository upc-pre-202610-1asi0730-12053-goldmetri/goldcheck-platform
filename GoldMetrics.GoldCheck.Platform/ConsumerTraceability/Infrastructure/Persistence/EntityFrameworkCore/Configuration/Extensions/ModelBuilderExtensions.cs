using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyConsumerTraceabilityConfiguration(this ModelBuilder builder)
    {
        // ── JewelryProduct ────────────────────────────────────────────────────

        builder.Entity<JewelryProduct>().HasKey(p => p.Id);
        builder.Entity<JewelryProduct>().Property(p => p.Id)
            .IsRequired().ValueGeneratedOnAdd();

        builder.Entity<JewelryProduct>().OwnsOne(p => p.QRCode, qr =>
        {
            qr.WithOwner().HasForeignKey("Id");
            qr.Property(v => v.Value)
                .HasColumnName("QRCode").IsRequired().HasMaxLength(200);
        });

        builder.Entity<JewelryProduct>().OwnsOne(p => p.ConsumerId, cid =>
        {
            cid.WithOwner().HasForeignKey("Id");
            cid.Property(v => v.Value)
                .HasColumnName("ConsumerId").IsRequired().HasMaxLength(100);
        });

        builder.Entity<JewelryProduct>().Property(p => p.Status)
            .HasColumnName("Status").IsRequired().HasMaxLength(50);

        builder.Entity<JewelryProduct>().Property(p => p.ScanCount)
            .HasColumnName("ScanCount").IsRequired();

        // ── TraceabilityJourney ───────────────────────────────────────────────

        builder.Entity<TraceabilityJourney>().HasKey(j => j.Id);
        builder.Entity<TraceabilityJourney>().Property(j => j.Id)
            .IsRequired().ValueGeneratedOnAdd();

        builder.Entity<TraceabilityJourney>().OwnsOne(j => j.ProductQRCode, qr =>
        {
            qr.WithOwner().HasForeignKey("Id");
            qr.Property(v => v.Value)
                .HasColumnName("ProductQRCode").IsRequired().HasMaxLength(200);
        });

        builder.Entity<TraceabilityJourney>().OwnsOne(j => j.ConsumerId, cid =>
        {
            cid.WithOwner().HasForeignKey("Id");
            cid.Property(v => v.Value)
                .HasColumnName("ConsumerId").IsRequired().HasMaxLength(100);
        });

        builder.Entity<TraceabilityJourney>().Property(j => j.JourneySummary)
            .HasColumnName("JourneySummary").IsRequired().HasMaxLength(500);

        builder.Entity<TraceabilityJourney>().Property(j => j.Status)
            .HasColumnName("Status").IsRequired().HasMaxLength(50);
    }
}