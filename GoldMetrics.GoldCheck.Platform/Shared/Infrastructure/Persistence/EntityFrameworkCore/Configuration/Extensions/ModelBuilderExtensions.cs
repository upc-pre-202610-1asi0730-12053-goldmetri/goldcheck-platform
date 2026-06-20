using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName)) entity.SetTableName(tableName.ToPlural().ToSnakeCase());

            foreach (var property in entity.GetProperties())
                property.SetColumnName(property.GetColumnName().ToSnakeCase());

            foreach (var key in entity.GetKeys())
            {
                var keyName = key.GetName();
                if (!string.IsNullOrEmpty(keyName)) key.SetName(keyName.ToSnakeCase());
            }

            foreach (var foreignKey in entity.GetForeignKeys())
            {
                var foreignKeyName = foreignKey.GetConstraintName();
                if (!string.IsNullOrEmpty(foreignKeyName)) foreignKey.SetConstraintName(foreignKeyName.ToSnakeCase());
            }

            foreach (var index in entity.GetIndexes())
            {
                var indexDatabaseName = index.GetDatabaseName();
                if (!string.IsNullOrEmpty(indexDatabaseName)) index.SetDatabaseName(indexDatabaseName.ToSnakeCase());
            }
        }
        builder.Entity<TemperatureReading>().HasKey(r => r.Id);
        builder.Entity<TemperatureReading>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<TemperatureReading>().OwnsOne(r => r.AssetId, aid =>
        {
            aid.WithOwner().HasForeignKey("Id");
            aid.Property(v => v.Value).HasColumnName("AssetId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<TemperatureReading>().Property(r => r.Status).IsRequired().HasMaxLength(50);
        builder.Entity<TemperatureReading>().Property(r => r.ExhaustCelsius).HasColumnType("decimal(8,2)");
        builder.Entity<TemperatureReading>().Property(r => r.ExhaustLimitCelsius).HasColumnType("decimal(8,2)");
        builder.Entity<TemperatureReading>().Property(r => r.RefrigerantCelsius).HasColumnType("decimal(8,2)");
        builder.Entity<TemperatureReading>().Property(r => r.OilCelsius).HasColumnType("decimal(8,2)");
        builder.Entity<TemperatureReading>().Property(r => r.FuelCelsius).HasColumnType("decimal(8,2)");
        builder.Entity<TemperatureReading>().Property(r => r.AnomalyCelsius).HasColumnType("decimal(8,2)");
        builder.Entity<TemperatureReading>().Property(r => r.AnomalyType).HasMaxLength(50);
        builder.Entity<TemperatureReading>().Property(r => r.AnomalyDescription).HasMaxLength(500);
    }
}
