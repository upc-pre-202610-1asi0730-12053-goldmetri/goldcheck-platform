using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyMonitoringTelemetryConfiguration(this ModelBuilder builder)
    {
        // ── TelemetryData ─────────────────────────────────────────────────────

        builder.Entity<TelemetryData>().HasKey(d => d.Id);
        builder.Entity<TelemetryData>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<TelemetryData>().ToTable("telemetry_data");
        builder.Entity<TelemetryData>().OwnsOne(d => d.AssetId, aid =>
        {
            aid.WithOwner().HasForeignKey("Id");
            aid.Property(v => v.Value).HasColumnName("AssetId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<TelemetryData>().Property(d => d.TelemetryDataId).IsRequired().HasMaxLength(36);
        builder.Entity<TelemetryData>().Property(d => d.RawData).IsRequired();
        builder.Entity<TelemetryData>().Property(d => d.Status).IsRequired().HasMaxLength(50);
        
        // ── GNSSStatus ────────────────────────────────────────────────────────

        builder.Entity<GNSSStatus>().HasKey(s => s.Id);
        builder.Entity<GNSSStatus>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<GNSSStatus>().ToTable("gnss_statuses");
        builder.Entity<GNSSStatus>().OwnsOne(s => s.AssetId, aid =>
        {
            aid.WithOwner().HasForeignKey("Id");
            aid.Property(v => v.Value).HasColumnName("AssetId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<GNSSStatus>().Property(s => s.Status).IsRequired().HasMaxLength(50);
        builder.Entity<GNSSStatus>().Property(s => s.RestartReason).HasMaxLength(500);
        
        // ── PressureReading ───────────────────────────────────────────────────

        builder.Entity<PressureReading>().HasKey(r => r.Id);
        builder.Entity<PressureReading>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<PressureReading>().OwnsOne(r => r.AssetId, aid =>
        {
            aid.WithOwner().HasForeignKey("Id");
            aid.Property(v => v.Value).HasColumnName("AssetId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<PressureReading>().Property(r => r.Status).IsRequired().HasMaxLength(50);
        builder.Entity<PressureReading>().Property(r => r.OilFilterDifferenceBar).HasColumnType("decimal(8,4)");
        builder.Entity<PressureReading>().Property(r => r.OilPanBar).HasColumnType("decimal(8,4)");
        builder.Entity<PressureReading>().Property(r => r.AbsoluteEngineOilBar).HasColumnType("decimal(8,4)");
        builder.Entity<PressureReading>().Property(r => r.OilFilterBar).HasColumnType("decimal(8,4)");
        builder.Entity<PressureReading>().Property(r => r.AnomalyPressureType).HasMaxLength(50);
        builder.Entity<PressureReading>().Property(r => r.AnomalyDescription).HasMaxLength(500);
        
        // ── SpeedReading ──────────────────────────────────────────────────────

        builder.Entity<SpeedReading>().HasKey(r => r.Id);
        builder.Entity<SpeedReading>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SpeedReading>().OwnsOne(r => r.AssetId, aid =>
        {
            aid.WithOwner().HasForeignKey("Id");
            aid.Property(v => v.Value).HasColumnName("AssetId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<SpeedReading>().Property(r => r.Status).IsRequired().HasMaxLength(50);
        //builder.Entity<SpeedReading>().Property(r => r.CurrentSpeedKmPerHour).HasColumnType("decimal(8,2)");
        //builder.Entity<SpeedReading>().Property(r => r.SpeedLimitKmPerHour).HasColumnType("decimal(8,2)");
        //builder.Entity<SpeedReading>().Property(r => r.ViolationDescription).HasMaxLength(500);
       
        // ── TemperatureReading ──────────────────────────────────────────────────────
        builder.Entity<TemperatureReading>().Property(r => r.Status).IsRequired().HasMaxLength(50);
        builder.Entity<TemperatureReading>().Property(r => r.ExhaustCelsius).HasColumnType("decimal(8,2)");
        builder.Entity<TemperatureReading>().Property(r => r.ExhaustLimitCelsius).HasColumnType("decimal(8,2)");
        builder.Entity<TemperatureReading>().Property(r => r.RefrigerantCelsius).HasColumnType("decimal(8,2)");
        builder.Entity<TemperatureReading>().Property(r => r.OilCelsius).HasColumnType("decimal(8,2)");
        builder.Entity<TemperatureReading>().Property(r => r.FuelCelsius).HasColumnType("decimal(8,2)");
        builder.Entity<TemperatureReading>().Property(r => r.AnomalyCelsius).HasColumnType("decimal(8,2)");
        builder.Entity<TemperatureReading>().Property(r => r.AnomalyType).HasMaxLength(50);
        builder.Entity<TemperatureReading>().Property(r => r.AnomalyDescription).HasMaxLength(500);
        
        // ── CommunicationChannel ──────────────────────────────────────────────

        builder.Entity<CommunicationChannel>().HasKey(c => c.Id);
        builder.Entity<CommunicationChannel>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CommunicationChannel>().OwnsOne(c => c.AssetId, aid =>
        {
            aid.WithOwner().HasForeignKey("Id");
            aid.Property(v => v.Value).HasColumnName("AssetId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<CommunicationChannel>().Property(c => c.Status).IsRequired().HasMaxLength(50);
        builder.Entity<CommunicationChannel>().Property(c => c.LastAnalysedProtocol).HasMaxLength(50);
        builder.Entity<CommunicationChannel>().Property(c => c.AnomalyProtocol).HasMaxLength(50);
        builder.Entity<CommunicationChannel>().Property(c => c.AnomalyDescription).HasMaxLength(500);
    }
}