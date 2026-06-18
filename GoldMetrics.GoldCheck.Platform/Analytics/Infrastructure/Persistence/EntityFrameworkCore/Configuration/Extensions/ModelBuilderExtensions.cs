using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAnalyticsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Material>().HasKey(m => m.Id);
        builder.Entity<Material>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Material>().Property(m => m.ProductionStart).HasColumnName("ProductionStart");
        builder.Entity<Material>().Property(m => m.ProductionEnd).HasColumnName("ProductionEnd");
        builder.Entity<Material>().Property(m => m.ProductionTons).HasColumnName("ProductionTons");

        builder.Entity<Material>().OwnsOne(m => m.MaterialId, mid =>
        {
            mid.WithOwner().HasForeignKey("Id");
            mid.Property(x => x.Value).HasColumnName("MaterialId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<Material>().OwnsOne(m => m.RouteId, rid =>
        {
            rid.WithOwner().HasForeignKey("Id");
            rid.Property(x => x.Value).HasColumnName("RouteId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<Material>().OwnsOne(m => m.RouteStatus, rs =>
        {
            rs.WithOwner().HasForeignKey("Id");
            rs.Property(x => x.Value).HasColumnName("RouteStatus").IsRequired().HasMaxLength(50);
        });
        builder.Entity<Material>().OwnsOne(m => m.SupervisorId, sid =>
        {
            sid.WithOwner().HasForeignKey("Id");
            sid.Property(x => x.Value).HasColumnName("SupervisorId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<Material>().OwnsOne(m => m.UserId, uid =>
        {
            uid.WithOwner().HasForeignKey("Id");
            uid.Property(x => x.Value).HasColumnName("UserId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<Material>().Property(m => m.Status).IsRequired().HasMaxLength(50);
        builder.Entity<Material>().Property(m => m.ProductionTons).HasColumnName("ProductionTons");

    }
}
