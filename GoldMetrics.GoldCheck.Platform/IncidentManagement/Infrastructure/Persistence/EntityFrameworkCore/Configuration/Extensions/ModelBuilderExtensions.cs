using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIncidentManagementConfiguration(this ModelBuilder builder)
    {
        builder.Entity<SafetyRecord>().HasKey(s => s.Id);
        builder.Entity<SafetyRecord>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SafetyRecord>().OwnsOne(s => s.IncidentType, it =>
        {
            it.WithOwner().HasForeignKey("Id");
            it.Property(x => x.Value).HasColumnName("IncidentType").IsRequired().HasMaxLength(50);
        });
        builder.Entity<SafetyRecord>().OwnsOne(s => s.OperatorId, oid =>
        {
            oid.WithOwner().HasForeignKey("Id");
            oid.Property(x => x.Value).HasColumnName("OperatorId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<SafetyRecord>().OwnsOne(s => s.AssetId, aid =>
        {
            aid.WithOwner().HasForeignKey("Id");
            aid.Property(x => x.Value).HasColumnName("AssetId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<SafetyRecord>().OwnsOne(s => s.RiskLevel, rl =>
        {
            rl.WithOwner().HasForeignKey("Id");
            rl.Property(x => x.Value).HasColumnName("RiskLevel").IsRequired().HasMaxLength(50);
        });
        builder.Entity<SafetyRecord>().Property(s => s.Description).HasMaxLength(500);
        builder.Entity<SafetyRecord>().Property(s => s.Status).IsRequired().HasMaxLength(50);
    }
}