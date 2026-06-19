using GoldMetrics.GoldCheck.Platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        
        // Iam Context
        builder.ApplyIAMConfiguration();
        builder.ApplyReportingNotificationsConfiguration();

        // MaterialOperations Context
        builder.ApplyMaterialOperationsConfiguration();

        // JewelryInventory Context
        builder.ApplyJewelryInventoryConfiguration();

        // FleetOperations Context
        builder.ApplyFleetOperationsConfiguration();
        builder.UseSnakeCaseNamingConvention();
        
        // Analytics Context
        builder.ApplyAnalyticsConfiguration();
        
        // AssetMaintenance Context
        builder.ApplyAssetMaintenanceConfiguration();
        
        // SubscriptionsAndBilling Context
        builder.ApplySubscriptionsBillingConfiguration();
    }
}