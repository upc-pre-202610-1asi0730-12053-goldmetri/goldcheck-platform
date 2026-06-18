using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAssetMaintenanceConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Machinery>().HasKey(m => m.Id);
        builder.Entity<Machinery>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Machinery>().OwnsOne(m => m.MachineryId, mid =>
        {
            mid.WithOwner().HasForeignKey("Id");
            mid.Property(x => x.Value).HasColumnName("MachineryId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<Machinery>().Property(m => m.Model).IsRequired().HasMaxLength(200);
        builder.Entity<Machinery>().Property(m => m.OEM).IsRequired().HasMaxLength(200);
        builder.Entity<Machinery>().OwnsOne(m => m.EngineHours, eh =>
        {
            eh.WithOwner().HasForeignKey("Id");
            eh.Property(x => x.Hours).HasColumnName("EngineHours").IsRequired();
        });
        builder.Entity<Machinery>().OwnsOne(m => m.MaintenanceStatus, ms =>
        {
            ms.WithOwner().HasForeignKey("Id");
            ms.Property(x => x.Value).HasColumnName("MaintenanceStatus").IsRequired().HasMaxLength(50);
        });
        builder.Entity<Machinery>().Property(m => m.Status).IsRequired().HasMaxLength(50);
        builder.Entity<Machinery>().Property(m => m.MaintenanceScheduledAtHours).HasColumnName("MaintenanceScheduledAtHours");
        builder.Entity<Machinery>().Property(m => m.DischargeReason).HasMaxLength(500);
    }
}